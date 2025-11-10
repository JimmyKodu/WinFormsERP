# WinFormsERP Technical Documentation

## Table of Contents

1. [System Architecture](#system-architecture)
2. [Database Design](#database-design)
3. [Core Components](#core-components)
4. [API Reference](#api-reference)
5. [Security](#security)
6. [Performance Optimization](#performance-optimization)

## System Architecture

### Layered Architecture

The application follows a layered architecture pattern:

```
┌─────────────────────────────────────┐
│     Presentation Layer (UI)         │
│    Windows Forms / MVP Pattern      │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Business Logic Layer           │
│         (Services)                  │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Data Access Layer              │
│  (Repositories + Unit of Work)      │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│         Database Layer              │
│      SQLite with EF Core            │
└─────────────────────────────────────┘
```

### Project Dependencies

- **WinFormsERP.UI** → WinFormsERP.Services, WinFormsERP.Data, WinFormsERP.Core
- **WinFormsERP.Services** → WinFormsERP.Data, WinFormsERP.Core
- **WinFormsERP.Data** → WinFormsERP.Core
- **WinFormsERP.Core** → (no dependencies - pure domain layer)

## Database Design

### Entity Relationship Overview

#### User Management
- User (1) ←→ (M) UserRole (M) ←→ (1) Role
- Role (1) ←→ (M) RolePermission (M) ←→ (1) Permission

#### Sales
- Customer (1) ←→ (M) SalesOrder (1) ←→ (M) SalesOrderItem (M) ←→ (1) Product

#### Purchasing
- Supplier (1) ←→ (M) PurchaseOrder (1) ←→ (M) PurchaseOrderItem (M) ←→ (1) Product

#### Inventory
- Product (1) ←→ (M) ProductStock (M) ←→ (1) Warehouse
- StockTransfer (1) ←→ (M) StockTransferItem (M) ←→ (1) Product
- StockTransfer (M) ←→ (1) FromWarehouse
- StockTransfer (M) ←→ (1) ToWarehouse

#### Finance
- Account (1) ←→ (M) SubAccounts (self-referencing hierarchy)
- JournalEntry (1) ←→ (M) JournalEntryLine (M) ←→ (1) Account
- Customer (1) ←→ (M) AccountReceivable (M) ←→ (1) SalesOrder
- Supplier (1) ←→ (M) AccountPayable (M) ←→ (1) PurchaseOrder

#### Human Resources
- Employee (1) ←→ (M) Attendance
- Employee (1) ←→ (M) Payroll

### Base Entity Pattern

All entities inherit from `BaseEntity`:

```csharp
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
```

This provides:
- Automatic audit trail
- Soft delete support
- Consistent primary key strategy

### Soft Delete Implementation

The system implements soft delete using a global query filter:

```csharp
modelBuilder.Entity<BaseEntity>()
    .HasQueryFilter(e => !e.IsDeleted);
```

To query including deleted records:
```csharp
context.Users.IgnoreQueryFilters().Where(u => u.IsDeleted);
```

## Core Components

### Repository Pattern

Generic repository provides common CRUD operations:

```csharp
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> CountAsync();
}
```

### Unit of Work Pattern

Manages transactions across multiple repositories:

```csharp
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
```

Example usage:
```csharp
await _unitOfWork.BeginTransactionAsync();
try
{
    await _repository.AddAsync(entity);
    await _unitOfWork.SaveChangesAsync();
    await _unitOfWork.CommitTransactionAsync();
}
catch
{
    await _unitOfWork.RollbackTransactionAsync();
    throw;
}
```

### Dependency Injection

Services are registered in `Program.cs`:

```csharp
// Database Context
services.AddDbContext<ERPDbContext>(options =>
    options.UseSqlite(connectionString));

// Repositories
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
services.AddScoped<IAuthenticationService, AuthenticationService>();
services.AddScoped<IUserService, UserService>();

// Forms
services.AddTransient<LoginForm>();
services.AddTransient<MainForm>();
```

## API Reference

### Authentication Service

#### AuthenticateAsync
```csharp
Task<User?> AuthenticateAsync(string username, string password)
```
Authenticates a user with username and password.

**Returns**: User object if successful, null otherwise.

#### ChangePasswordAsync
```csharp
Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
```
Changes user password after verifying old password.

**Returns**: True if successful, false otherwise.

### User Service

#### CreateUserAsync
```csharp
Task<User> CreateUserAsync(User user)
```
Creates a new user in the system.

#### GetUserByIdAsync
```csharp
Task<User?> GetUserByIdAsync(int id)
```
Retrieves user by ID with roles included.

#### AssignRoleAsync
```csharp
Task<bool> AssignRoleAsync(int userId, int roleId)
```
Assigns a role to a user.

## Security

### Password Hashing

Passwords are hashed using SHA256 (for demonstration). In production, use BCrypt or Argon2:

```csharp
public string HashPassword(string password)
{
    return Convert.ToBase64String(
        SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}
```

**Recommendation**: Replace with BCrypt.Net-Next package:
```csharp
Install-Package BCrypt.Net-Next

public string HashPassword(string password)
{
    return BCrypt.Net.BCrypt.HashPassword(password);
}

public bool VerifyPassword(string password, string hash)
{
    return BCrypt.Net.BCrypt.Verify(password, hash);
}
```

### Role-Based Access Control

Permissions are checked through the relationship:
User → UserRole → Role → RolePermission → Permission

```csharp
var hasPermission = user.UserRoles
    .SelectMany(ur => ur.Role.RolePermissions)
    .Any(rp => rp.Permission.Name == "CREATE_SALES_ORDER");
```

### SQL Injection Prevention

Entity Framework Core uses parameterized queries automatically, preventing SQL injection:

```csharp
// Safe - EF Core parameterizes
var user = await context.Users
    .FirstOrDefaultAsync(u => u.Username == username);
```

## Performance Optimization

### Database Indexing

Key indexes to create:

```sql
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_SalesOrders_CustomerId ON SalesOrders(CustomerId);
CREATE INDEX IX_SalesOrders_OrderDate ON SalesOrders(OrderDate);
CREATE INDEX IX_ProductStock_ProductId ON ProductStock(ProductId);
CREATE INDEX IX_ProductStock_WarehouseId ON ProductStock(WarehouseId);
```

### Lazy Loading vs Eager Loading

Use explicit loading for better performance:

```csharp
// Eager loading - loads related data in single query
var user = await context.Users
    .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
    .FirstOrDefaultAsync(u => u.Id == userId);
```

### Caching Strategy

Implement caching for frequently accessed data:

```csharp
private readonly IMemoryCache _cache;

public async Task<IEnumerable<Permission>> GetPermissionsAsync()
{
    return await _cache.GetOrCreateAsync("permissions", async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
        return await _context.Permissions.ToListAsync();
    });
}
```

### Connection Pooling

SQLite connection pooling is enabled by default:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=erp.db;Cache=Shared;Pooling=True"
  }
}
```

### Asynchronous Operations

All database operations use async/await for better scalability:

```csharp
// Good - async
var users = await _context.Users.ToListAsync();

// Bad - synchronous (blocks thread)
var users = _context.Users.ToList();
```

## Error Handling

### Global Exception Handling

Implement in forms:

```csharp
Application.ThreadException += (sender, e) =>
{
    MessageBox.Show($"An error occurred: {e.Exception.Message}",
        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    
    // Log exception
    LogException(e.Exception);
};
```

### Service Layer Exceptions

```csharp
public async Task<User> CreateUserAsync(User user)
{
    try
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    catch (DbUpdateException ex)
    {
        // Handle database-specific errors
        throw new InvalidOperationException("Failed to create user", ex);
    }
}
```

## Testing Strategy

### Unit Tests

Test services in isolation using in-memory database:

```csharp
var options = new DbContextOptionsBuilder<ERPDbContext>()
    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
    .Options;

var context = new ERPDbContext(options);
var service = new UserService(context);
```

### Integration Tests

Test full stack including database:

```csharp
[Fact]
public async Task CreateUser_WithValidData_PersistsToDatabase()
{
    // Arrange
    var user = new User { ... };
    
    // Act
    await _userService.CreateUserAsync(user);
    
    // Assert
    var savedUser = await _context.Users.FindAsync(user.Id);
    Assert.NotNull(savedUser);
}
```

## Deployment

### Prerequisites
- Windows Server 2016 or later
- .NET 8 Runtime
- 2GB+ RAM
- 1GB+ disk space

### Installation Steps

1. Publish the application:
   ```bash
   dotnet publish -c Release -r win-x64 --self-contained
   ```

2. Copy files to target server

3. Run the application:
   ```bash
   WinFormsERP.UI.exe
   ```

### Database Backup

Regular backups of SQLite database:

```bash
# Copy database file
copy erp.db erp_backup_%date%.db

# Or use SQLite backup command
sqlite3 erp.db ".backup erp_backup.db"
```

## Maintenance

### Database Maintenance

```sql
-- Vacuum database to reclaim space
VACUUM;

-- Analyze tables for query optimization
ANALYZE;

-- Check integrity
PRAGMA integrity_check;
```

### Logging

Implement logging using Microsoft.Extensions.Logging:

```csharp
services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddFile("logs/erp-{Date}.txt");
});
```

## Troubleshooting

### Common Issues

**Issue**: Database is locked
**Solution**: Ensure only one connection is open at a time, or enable WAL mode:
```sql
PRAGMA journal_mode=WAL;
```

**Issue**: Slow queries
**Solution**: Analyze query execution and add appropriate indexes

**Issue**: Memory leaks
**Solution**: Ensure proper disposal of DbContext and other IDisposable objects

---

For more information, visit the project repository or contact the development team.
