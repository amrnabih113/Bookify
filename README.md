# 🏨 Bookify - Hotel Room Booking System

[![DEPI Graduation Project](https://img.shields.io/badge/DEPI-Graduation%20Project-blue)](https://github.com/amrnabih113/Bookify)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4)](https://docs.microsoft.com/en-us/aspnet/core/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4)](https://docs.microsoft.com/en-us/ef/core/)

**Bookify** is a comprehensive hotel room booking management system developed as a graduation project for the **Digital Egypt Pioneers Initiative (DEPI)** program. This web application provides a seamless experience for users to browse, book, and manage hotel room reservations, while offering administrators powerful tools to manage the entire booking ecosystem.

---

## 📋 Table of Contents

- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Prerequisites](#-prerequisites)
- [Installation](#-installation)
- [Configuration](#-configuration)
- [Database Setup](#-database-setup)
- [Usage](#-usage)
- [Project Structure](#-project-structure)
- [Default Credentials](#-default-credentials)
- [Team Members](#-team-members)
- [Contributing](#-contributing)
- [License](#-license)

---

## ✨ Features

### 🔐 User Management
- **User Registration & Authentication**: Secure user registration and login system with ASP.NET Core Identity
- **Google OAuth Integration**: Quick sign-in with Google accounts
- **Role-Based Access Control**: Admin and User roles with different permissions
- **Profile Management**: Users can manage their profiles and preferences

### 🏨 Room Management
- **Room Browsing**: Browse available rooms with detailed information
- **Room Types**: Multiple room types (Single, Double, Suite, Deluxe, etc.)
- **Room Amenities**: View and filter rooms by amenities (WiFi, TV, AC, etc.)
- **Room Images**: Multiple images per room with gallery view
- **Price Management**: Dynamic pricing with discount support
- **Room Availability**: Real-time availability checking

### 📅 Booking System
- **Online Booking**: Easy-to-use booking interface with date selection
- **Booking Management**: Users can view, manage, and cancel their bookings
- **Payment Integration**: Multiple payment methods support
- **Payment Status Tracking**: Track payment status (Pending, Confirmed, Cancelled)
- **Booking History**: Complete booking history for users
- **Date Validation**: Prevents double bookings and validates date ranges

### ⭐ Reviews & Ratings
- **Room Reviews**: Users can leave reviews and ratings for rooms
- **Review Management**: View and manage all reviews

### ❤️ Favorites System
- **Wishlist**: Users can add rooms to their favorites for easy access
- **Favorite Management**: View and manage favorite rooms

### 👨‍💼 Admin Panel
- **Dashboard**: Comprehensive admin dashboard with statistics
- **Room Management**: Add, edit, and delete rooms
- **Booking Management**: View and manage all bookings
- **User Management**: Manage user accounts and roles
- **Amenity Management**: Manage room amenities
- **Room Type Management**: Create and manage room types

### 📱 Additional Features
- **Responsive Design**: Mobile-friendly interface
- **Contact Form**: Contact page for user inquiries
- **About Page**: Information about the hotel
- **Static File Caching**: Optimized performance with browser caching
- **Email Notifications**: Email sender service for notifications
- **Database Seeding**: Automatic data seeding for quick setup

---

## 🛠 Technology Stack

### Backend
- **Framework**: ASP.NET Core 8.0 (MVC)
- **Language**: C# 12
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity
- **OAuth**: Google OAuth 2.0

### Frontend
- **UI Framework**: Bootstrap 5
- **JavaScript**: jQuery
- **Validation**: jQuery Validation
- **Icons**: Font Awesome / Bootstrap Icons
- **CSS**: Custom CSS with Bootstrap

### Architecture & Patterns
- **Pattern**: Repository Pattern
- **Dependency Injection**: Built-in ASP.NET Core DI
- **Service Layer**: Separation of business logic
- **View Models**: Data transfer objects for views
- **Custom Validation**: Attribute-based validation

### Development Tools
- **IDE**: Visual Studio 2022 / Visual Studio Code
- **Version Control**: Git & GitHub
- **Package Manager**: NuGet

---

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

- **[.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** or later
- **[SQL Server 2019](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)** or later (LocalDB, Express, or full version)
- **[Visual Studio 2022](https://visualstudio.microsoft.com/)** or **[Visual Studio Code](https://code.visualstudio.com/)** (recommended)
- **[Git](https://git-scm.com/)** for version control

---

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/amrnabih113/Bookify.git
cd Bookify
```

### 2. Navigate to Project Directory

```bash
cd Bookify/Bookify
```

### 3. Restore NuGet Packages

```bash
dotnet restore
```

---

## ⚙️ Configuration

### 1. Update Connection String

Open `appsettings.json` and update the connection string to point to your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=Bookify_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;"
  }
}
```

**Connection String Options:**
- **LocalDB**: `Server=(localdb)\\mssqllocaldb;Database=Bookify_DB;Trusted_Connection=True;`
- **SQL Server Express**: `Server=.\\SQLEXPRESS;Database=Bookify_DB;Integrated Security=True;`
- **SQL Server**: `Server=localhost;Database=Bookify_DB;Integrated Security=True;`

### 2. Configure Google OAuth (Optional)

If you want to enable Google authentication, update the following section in `appsettings.json`:

```json
{
  "Authentication": {
    "Google": {
      "ClientId": "your-google-client-id",
      "ClientSecret": "your-google-client-secret"
    }
  }
}
```

**How to get Google OAuth credentials:**
1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project or select an existing one
3. Enable Google Identity Services (OAuth 2.0)
4. Create OAuth 2.0 credentials (OAuth client ID)
5. Add authorized redirect URI: `https://localhost:5001/signin-google`

---

## 💾 Database Setup

### Automatic Setup (Recommended)

The application automatically creates and seeds the database on first run. Simply run the application:

```bash
dotnet run
```

The application will:
1. Create the database if it doesn't exist
2. Apply all migrations
3. Seed initial data (roles, admin account, room types, rooms, amenities)

### Manual Setup (Optional)

If you prefer manual setup or need to reset the database:

```bash
# Create a new migration
dotnet ef migrations add InitialCreate

# Update the database
dotnet ef database update
```

### Database Seeding

The application seeds the following data automatically:
- **Roles**: Admin, User
- **Admin Account**: admin@bookify.com / Admin@123
- **Room Types**: Single, Double, Suite, Deluxe, etc.
- **Amenities**: WiFi, TV, AC, Mini Bar, etc.
- **Sample Rooms**: Multiple rooms with images and amenities

---

## 🎯 Usage

### Running the Application

1. **Using Visual Studio:**
   - Open `Bookify.sln`
   - Press `F5` or click the "Run" button

2. **Using .NET CLI:**
   ```bash
   dotnet run
   ```

3. **Using Visual Studio Code:**
   ```bash
   dotnet watch run
   ```

The application will start on:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000

### First Steps

1. **Access the Application**: Navigate to `https://localhost:5001`
2. **Register a User**: Create a new user account or use Google sign-in
3. **Browse Rooms**: Explore available rooms and their details
4. **Make a Booking**: Select dates and book a room
5. **Admin Access**: Login with admin credentials to access the admin panel

---

## 📁 Project Structure

```
Bookify/
├── Controllers/           # MVC Controllers
│   ├── AccountController.cs      # Authentication & user management
│   ├── AdminController.cs        # Admin panel operations
│   ├── BookingController.cs      # Booking management
│   ├── FavoriteController.cs     # Favorites management
│   ├── HomeController.cs         # Home, About, Contact pages
│   ├── ReviewController.cs       # Review management
│   └── RoomController.cs         # Room browsing & details
├── Models/                # Data Models
│   ├── Amenity.cs
│   ├── ApplicationUser.cs
│   ├── Booking.cs
│   ├── Favorite.cs
│   ├── Review.cs
│   ├── Room.cs
│   ├── RoomAmenity.cs
│   ├── RoomImage.cs
│   ├── RoomType.cs
│   └── ViewModels/        # View Models for data transfer
├── Views/                 # Razor Views
│   ├── Account/           # Login, Register, Profile views
│   ├── Admin/             # Admin panel views
│   ├── Booking/           # Booking views
│   ├── Home/              # Home, About, Contact views
│   ├── Room/              # Room listing & details views
│   └── Shared/            # Shared layouts & partials
├── Data/                  # Database Context & Seeders
│   ├── ApplicationDbContext.cs
│   └── IdentitySeeder.cs
├── Repository/            # Repository Pattern Implementation
│   ├── IGenericRepository.cs
│   ├── GenericRepository.cs
│   ├── IRoomRepository.cs
│   ├── RoomRepository.cs
│   ├── IFavoriteRepository.cs
│   └── FavoriteRepository.cs
├── services/              # Business Logic Layer
│   ├── IServices/         # Service interfaces
│   ├── BookingService.cs
│   ├── AmenityService.cs
│   ├── RoomService.cs
│   ├── RoomTypeService.cs
│   └── FavoriteService.cs
├── Custom Validation/     # Custom validation attributes
├── wwwroot/               # Static files
│   ├── css/               # Stylesheets
│   ├── js/                # JavaScript files
│   ├── images/            # Images & room photos
│   └── lib/               # Client-side libraries
├── Migrations/            # EF Core Migrations
├── SQL_Scripts/           # SQL scripts for database
├── appsettings.json       # Application configuration
└── Program.cs             # Application entry point
```

---

## 🔑 Default Credentials

### Admin Account
- **Email**: `admin@bookify.com`
- **Password**: `Admin@123`

**Note**: Please change the default admin password after first login for security reasons.

---

## 👥 Team Members

This project was developed as part of the **DEPI (Digital Egypt Pioneers Initiative)** graduation project.

**Development Team:**
- [Amr Nabih](https://github.com/amrnabih113) - Team Lead & Full Stack Developer
- [Saleh Mostafa](https://github.com/salehmostafa11) - Full Stack Developer
- [Mahmoud Elshiha](https://github.com/MahmoudElshiha) - Full Stack Developer
- [Moaz Mostafa](https://github.com/moazzehry) - Full Stack Developer

**Project Supervisor:** DEPI Program

**Institution:** Digital Egypt Pioneers Initiative (DEPI)

---

## 🤝 Contributing

We welcome contributions to improve Bookify! Here's how you can help:

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Commit your changes**: `git commit -m 'Add some amazing feature'`
4. **Push to the branch**: `git push origin feature/amazing-feature`
5. **Open a Pull Request**

### Code Standards
- Follow C# coding conventions
- Write meaningful commit messages
- Add comments for complex logic
- Ensure all tests pass before submitting
- Update documentation as needed

---

## 📄 License

This project is developed as part of the DEPI graduation requirements. All rights reserved to the development team and DEPI program.

---

## 📧 Contact & Support

For questions, feedback, or support:

- **GitHub Issues**: [Create an issue](https://github.com/amrnabih113/Bookify/issues)
- **Email**: Contact through GitHub profiles

---

## 🙏 Acknowledgments

- **Digital Egypt Pioneers Initiative (DEPI)** for providing the opportunity and support
- **ASP.NET Core Community** for excellent documentation and resources
- **All contributors** who helped make this project possible

---

## 📸 Screenshots

*Coming soon - Screenshots of the application will be added here*

---

<div align="center">
  <p>Made with ❤️ by the Bookify Team</p>
  <p>© 2024 Bookify - DEPI Graduation Project</p>
</div>
