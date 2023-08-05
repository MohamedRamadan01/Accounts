# Project Name

Welcome to the Accounts task! This project is built using .NET 6 and utilizes a Microsoft Access database for data storage.

## Getting Started

To run this project locally, please follow these steps:

### Project Structure

The solution is organized into the following projects:

- `Accounts.Api`: The entry point for the microservice, responsible for handling incoming HTTP requests and routing them to the appropriate application logic.
- `Accounts.Application`: Contains the business logic and use cases of the microservice. This layer orchestrates data flow and business rules.
- `Accounts.Core`: Defines the core domain models, entities, and interfaces that represent the fundamental concepts of the Accounts microservice.
- `Accounts.Infrastructure`: Implements the data access layer, including repositories and unit of work using Entity Framework Core to interact with the Microsoft Access database.
- `Accounts.UnitTest`: Contains unit tests for testing various components of the microservice.

### Prerequisites

1. Install .NET 6 SDK on your machine. You can download it from [here](https://dotnet.microsoft.com/download/dotnet/6.0).

### Installation

1. Clone this repository to your local machine.
2. Navigate to the project directory: `cd Accounts`.
3. Build the project: `dotnet build`.

### Database Setup

1. Open Microsoft Access and create a new database or use the attached one.
2. Make sure you have the necessary tables and schema set up to match the expected data structure of the application.

### Configuration

1. Open the `appsettings.json` file in the project.
2. Update the `ConnectionString` to point to your Microsoft Access database file.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mhmdr\\OneDrive\\Documents\\accountsdb.accdb"
  },
  // Other settings...
}

