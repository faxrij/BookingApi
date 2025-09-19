# Booking API

## Overview

This API allows users to retrieve a list of homes available for a given date range. Each home has unique availability slots (dates), and the API returns only homes that are fully available for the requested range.

- **Endpoint:** `GET /api/available-homes`
- **Input:** `startDate` and `endDate` (YYYY-MM-DD)
- **Output:** List of homes with `homeId`, `homeName`, and available slots within the requested range.
- **Storage:** In-memory (no database)

---

## How to Run the Application

### Prerequisites

- .NET 8 SDK

### Steps

1. Clone the repository:

    ```bash
    git clone https://github.com/yourusername/BookingApi.git
    cd BookingApi
    ```

2. Build and run the API:

    ```bash
   cd BookingApi dotnet run
   ```

The API will start on `https://localhost:5001` (or a dynamic port).

Navigate to `https://localhost:5001/swagger` to explore endpoints via Swagger UI.

How to Test the Application
---------------------------
## Integration Tests

* Integration tests verify the endpoint behavior, including:
* Valid date ranges
* Missing required fields
* Invalid date formats
* Business logic validation (startDate >= endDate)

Run the tests using the .NET CLI:

    cd BookingApi.IntegrationTests
    dotnet test

Or via Visual Studio / Rider test explorer.

Architecture and Filtering Logic
--------------------------------

    BookingApi/
    ├─ Controllers/        # API endpoints
    ├─ Services/           # Business logic
    ├─ Repositories/       # In-memory data access
    ├─ DTOs/               # Data transfer objects
    ├─ Middlewares/        # Exception handling
    └─ Program.cs          # App startup

## Filtering Logic

1. Input Validation: startDate must be less than endDate. Otherwise, returns 400 Bad Request.

2. Date Range Construction: Generates all dates in the requested range.

3. Home Filtering: For each home, check if all dates in the requested range exist in AvailableSlots.

4. DTO Projection: Only include the slots within the requested range in the response.

5. Performance Consideration:
   * AvailableSlots are stored as HashSet<DateTime> cnally for fast lookups.
   * The API uses asynchronous programming to avoid blocking.

## Example Response

    {
        "status": "OK",
        "homes": [
          {
            "homeId": "123",
            "homeName": "Home 1",
            "availableSlots": ["2025-07-15", "2025-07-16"]
          },
          {
            "homeId": "456",
            "homeName": "Home 2",
            "availableSlots": ["2025-07-17", "2025-07-18"]
          }
        ]
    }

Notes
-----
* The system is designed to handle large in-memory datasets efficiently.

* Integration tests use a fake in-memory repository to simulate different scenarios.

* Middleware is used for exception handling to return JSON-formatted error responses.

Highlights
----------

* Clean separation of concerns (Controllers, Services, Repositories)

* Uses async programming for non-blocking operations

* HashSet<DateTime> for efficient date lookups

* Well-tested via integration tests covering multiple scenarios

* Follows clean architecture principles and SOLID design
