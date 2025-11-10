# WinFormsERP

A comprehensive Enterprise Resource Planning (ERP) system built with WinForms for small and medium-sized businesses.

## Overview

WinFormsERP is a desktop application built on .NET 8 framework using Windows Forms UI, Entity Framework Core for data access, and SQLite as the database. The system follows MVP (Model-View-Presenter) pattern with dependency injection for loose coupling.

## Features

### Core Modules

1. **User Rights Management**
   - User authentication and authorization
   - Role-based access control (RBAC)
   - Permission management

2. **Sales Module**
   - Customer management
   - Sales order processing
   - Sales analytics

3. **Purchasing Module**
   - Supplier management
   - Purchase order management
   - Supplier evaluation

4. **Inventory Module**
   - Product management
   - Multi-warehouse support
   - Stock transfers between warehouses
   - Low stock alerts

5. **Finance Module**
   - Chart of accounts
   - General ledger with journal entries
   - Accounts Receivable (AR)
   - Accounts Payable (AP)
   - Financial reports

6. **Human Resources Module**
   - Employee management
   - Attendance tracking
   - Payroll processing

## Architecture

### Technology Stack

- **Framework**: .NET 8
- **UI**: Windows Forms
- **Data Access**: Entity Framework Core 8.0
- **Database**: SQLite
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection
- **Testing**: xUnit with Moq

### Project Structure

```
WinFormsERP/
├── src/
│   ├── WinFormsERP.UI/          # Windows Forms application
│   ├── WinFormsERP.Core/         # Domain entities and interfaces
│   ├── WinFormsERP.Data/         # Data access layer with EF Core
│   └── WinFormsERP.Services/     # Business logic layer
├── tests/
│   └── WinFormsERP.Tests/        # Unit and integration tests
└── docs/                          # Documentation
```

### Design Patterns

- **MVP (Model-View-Presenter)**: Separates presentation logic from business logic
- **Repository Pattern**: Abstracts data access layer
- **Unit of Work Pattern**: Manages transactions across multiple repositories
- **Dependency Injection**: Promotes loose coupling and testability

## Getting Started

### Prerequisites

- .NET 8 SDK or later
- Windows OS (for WinForms)
- Visual Studio 2022 or Visual Studio Code (optional)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/JimmyKodu/WinFormsERP.git
   cd WinFormsERP
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   cd src/WinFormsERP.UI
   dotnet run
   ```

### Default Login Credentials

- **Username**: admin
- **Password**: admin123

⚠️ **Important**: Change the default password after first login!

## Running Tests

Run all tests with coverage:
```bash
dotnet test
```

Run tests with detailed output:
```bash
dotnet test --logger "console;verbosity=detailed"
```

## Database

The application uses SQLite as the database, which is automatically created on first run. The database file (`erp.db`) is stored in the application's output directory.

### Database Migration

Entity Framework Core migrations are used for database schema management:

```bash
# Create a new migration
dotnet ef migrations add MigrationName --project src/WinFormsERP.Data

# Apply migrations
dotnet ef database update --project src/WinFormsERP.Data
```

## Configuration

Application settings are stored in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=erp.db"
  }
}
```

## Development Roadmap

- [x] Phase 1: Project setup and basic architecture
- [x] Phase 2: Core domain models and database design
- [x] Phase 3: Authentication and user management
- [ ] Phase 4: Sales module implementation
- [ ] Phase 5: Purchasing module implementation
- [ ] Phase 6: Inventory module implementation
- [ ] Phase 7: Finance module implementation
- [ ] Phase 8: HR module implementation
- [ ] Phase 9: Third-party integrations (Payment gateways, E-invoicing, WeChat)
- [ ] Phase 10: Mobile application companion
- [ ] Phase 11: System integration and performance testing
- [ ] Phase 12: User acceptance testing and documentation
- [ ] Phase 13: Deployment and user training

## Quality Assurance

- **Code Reviews**: Every commit undergoes peer review
- **Unit Tests**: Target coverage of 80%+
- **Static Analysis**: SonarQube integration for code quality
- **Performance Testing**: Load testing for 200+ concurrent users

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is proprietary software. All rights reserved.

## Support

For technical support and inquiries:
- Email: support@erp.com
- Documentation: See `/docs` folder
- Issue Tracker: GitHub Issues

## System Requirements

### Minimum Requirements
- Windows 10 or later
- 2 GB RAM
- 500 MB available disk space
- .NET 8 Runtime

### Recommended Requirements
- Windows 11
- 4 GB RAM or more
- 1 GB available disk space
- Solid State Drive (SSD)

## Performance

- **Target Availability**: 99.5%
- **Concurrent Users**: Supports up to 200 users
- **Response Time**: < 2 seconds for standard operations

## Changelog

### Version 1.0.0 (Initial Release)
- Core project structure and architecture
- User authentication and authorization
- Database schema with all core entities
- Basic UI framework with navigation
- Unit test coverage for core services

---

© 2025 WinFormsERP. All Rights Reserved.
