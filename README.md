# 🎦 Film Ticket Booking System API

This is a .NET Core 8.0 Web API for a Film Ticket Booking System integrated with a PostgreSQL database using Entity Framework Core (EF Core). The system allows users to browse available films, select showtimes, hold tickets, and complete bookings. Administrators have additional permissions to manage films, showtimes, and view all bookings.

## Table of Contents 📋

- [Features](#features)
- [Technology Stack](#technology-stack)
- [Installation Guide](#installation-guide)
- [API Documentation](#api-documentation)
- [Challenges Faced](#challenges-faced)

---

## Features 🚀

### Film and Showtime Management

- **Admin-only Features:**

  - Create films with details such as name, description, duration, amount, genre and showtimes.
  - Manage seating availability per showtime.

- **User Features:**
  - Browse films, filter by name and genre
  - Retrieve a list of all films and showtimes.

### Ticket Booking

- **Hold Ticket Logic:**

  - Hold tickets for a set period (e.g., 10 minutes). If the booking is not completed in time, the hold expires, and tickets are re-available.

- **Order Management:**
  - Users can view past bookings and upcoming shows, while admins can track all ticket sales per film/showtime.

### Authentication and Authorization

- JWT-based authentication with two roles:
  - **User:** Can browse films, book tickets, and view booking history.
  - **Admin:** Can manage films, showtimes, and seating availability, and view all bookings.

---

## Technology Stack

- **Framework:** .NET Core 8.0
- **Database:** PostgreSQL with Entity Framework Core
- **Authentication:** JWT
- **Design Patterns:** Unit of Work with Genric Repository pattern, Dependency Injection
- **Documentation:** Swagger for API documentation as well as Postman Collection API documentation.

---

## Installation Guide

Follow these steps to set up and run the project locally.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/)
- [Visual Studio](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false)

### Steps

1. **Clone the Repository**

   ```bash
   git clone https://github.com/Ariyanayagan/FlimBookingSystem.git
   ```
2. **Navigate to the Project Directory**

   ```bash
   cd FlimBookingSystem
   ```
3.  **Set up Enivironment Variables** 
    In `appsettings.json `
    ```json
    "ConnectionStrings": {
        "PostgresConnection": "<your connection string>"
    },
    "Jwt": {
        "Key": "jdnskckndkcnkmdnckmdnmkcndsmkncmdncmdnckmdnckmndsckmnsdknmcd,ksmksmkdm",
        "Issuer": "Techno",
        "Audience": "Flimbook",
        "ExpiryInMinutes": 30
    }
    ```
4. **Apply Migrations**

    Open "Package Manager Console in Visual Studio and enter following commands"  In visual Studio Tools menu -> Nuget package manager -> Package manager console 

    ```
    Add-Migration "Initial-Migrate"
    Update-database
    ```
---
## API Documentation

> ## You can either use "Postman" or "Swagger". 
> ### For Swagger:
> Once the API is running, navigate to:
> ```plaintext
> https://localhost:5001/swagger
> ```
> ## For Postman: 
> - Download Postman Client from official Website.
> - In Postman, Go to collection and import a collection . [Download Postman Collection ✅](https://dotnet.microsoft.com/download/dotnet/8.0)

Swagger provides a user-friendly interface to test each endpoint, view request and response formats, and explore available functionality.

---

## Challenges Faced

- Concurrent Ticket Holding: Implementing a temporary hold on tickets required fine-tuned handling of concurrency to prevent double bookings.

