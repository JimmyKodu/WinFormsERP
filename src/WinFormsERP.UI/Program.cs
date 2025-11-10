using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WinFormsERP.Data.Context;
using WinFormsERP.Core.Interfaces.Services;
using WinFormsERP.Core.Interfaces.Repositories;
using WinFormsERP.Services.Authentication;
using WinFormsERP.Services.Users;
using WinFormsERP.Data.Repositories;
using WinFormsERP.UI.Forms.Login;

namespace WinFormsERP.UI;

static class Program
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        
        // Configure services
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        // Initialize database
        InitializeDatabase();

        // Run login form
        Application.Run(ServiceProvider.GetRequiredService<LoginForm>());
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);

        // Database Context
        services.AddDbContext<ERPDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection") 
                ?? "Data Source=erp.db"));

        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();

        // Forms
        services.AddTransient<LoginForm>();
        services.AddTransient<Forms.Main.MainForm>();
    }

    private static void InitializeDatabase()
    {
        using var scope = ServiceProvider!.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ERPDbContext>();
        
        // Ensure database is created
        context.Database.EnsureCreated();
        
        // Seed initial data if needed
        SeedData(context);
    }

    private static void SeedData(ERPDbContext context)
    {
        // Check if we already have data
        if (context.Users.Any())
        {
            return;
        }

        // Create default admin user
        var authService = new AuthenticationService(context);
        var adminUser = new Core.Entities.User
        {
            Username = "admin",
            PasswordHash = authService.HashPassword("admin123"),
            Email = "admin@erp.com",
            FullName = "System Administrator",
            Phone = "000-000-0000",
            IsActive = true
        };

        context.Users.Add(adminUser);

        // Create default role
        var adminRole = new Core.Entities.Role
        {
            Name = "Administrator",
            Description = "Full system access"
        };

        context.Roles.Add(adminRole);
        context.SaveChanges();

        // Assign role to admin user
        context.UserRoles.Add(new Core.Entities.UserRole
        {
            UserId = adminUser.Id,
            RoleId = adminRole.Id
        });

        context.SaveChanges();
    }
}
