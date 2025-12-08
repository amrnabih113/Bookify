using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomSerivce _roomService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            IBookingService bookingService,
            IRoomSerivce roomService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _bookingService = bookingService;
            _roomService = roomService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var allBookings = await _bookingService.GetAllAsync();
            var allRooms = await _roomService.GetAllForAdminAsync();
            var user = await _userManager.GetUserAsync(User);

            var model = new AdminDashboardViewModel
            {
                AdminName = user != null ? user.FullName : "Admin",
                TotalBookings = allBookings.Count(),
                ConfirmedBookings = allBookings.Count(b => b.PaymentStatus == "Confirmed"),
                AvailableRooms = allRooms.Count(r => r.IsAvailable),
                TotalRevenue = allBookings
                                .Where(b => b.PaymentStatus == "Confirmed" || b.PaymentStatus == "Completed")
                                .Sum(b => b.TotalPrice),
                RecentBookings = allBookings.OrderByDescending(b => b.CreatedAt).Take(5),
                AllBookings = allBookings.OrderByDescending(b => b.CreatedAt),
                Rooms = allRooms
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            var adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                return Json(new { success = false, message = "Admin role not found" });
            }

            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var adminList = admins.Select(a => new
            {
                id = a.Id,
                fullName = a.FullName,
                email = a.Email,
                emailConfirmed = a.EmailConfirmed
            });

            return Json(new { success = true, admins = adminList });
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View(new CreateAdminViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(CreateAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "A user with this email already exists");
                return View(model);
            }

            // Create new admin user
            var adminUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(adminUser, model.Password);
            if (result.Succeeded)
            {
                // Assign Admin role
                var roleResult = await _userManager.AddToRoleAsync(adminUser, "Admin");
                if (roleResult.Succeeded)
                {
                    TempData["Success"] = $"Admin account for {model.FullName} created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    // If role assignment fails, delete the user
                    await _userManager.DeleteAsync(adminUser);
                    ModelState.AddModelError("", "Failed to assign admin role");
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBookingStatus([FromBody] UpdateBookingStatusRequest request)
        {
            try
            {
                if (request == null || request.BookingId <= 0)
                {
                    return Json(new { success = false, message = "Invalid request" });
                }

                var validStatuses = new[] { "Pending", "Confirmed", "Cancelled", "Completed" };
                if (!validStatuses.Contains(request.Status))
                {
                    return Json(new { success = false, message = "Invalid status" });
                }

                var booking = await _bookingService.GetByIdAsync(request.BookingId);
                if (booking == null)
                {
                    return Json(new { success = false, message = "Booking not found" });
                }

                booking.PaymentStatus = request.Status;
                await _bookingService.UpdateBookingAsync(booking);

                return Json(new { success = true, message = "Status updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooking([FromBody] DeleteBookingRequest request)
        {
            try
            {
                if (request == null || request.BookingId <= 0)
                {
                    return Json(new { success = false, message = "Invalid request" });
                }

                var booking = await _bookingService.GetByIdAsync(request.BookingId);
                if (booking == null)
                {
                    return Json(new { success = false, message = "Booking not found" });
                }

                if (booking.PaymentStatus != "Cancelled")
                {
                    return Json(new { success = false, message = "Only cancelled bookings can be deleted" });
                }

                var deleted = await _bookingService.DeleteBookingAsync(request.BookingId);
                if (deleted)
                {
                    return Json(new { success = true, message = "Booking deleted successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete booking" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

    public class UpdateBookingStatusRequest
    {
        public int BookingId { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class DeleteBookingRequest
    {
        public int BookingId { get; set; }
    }
}
