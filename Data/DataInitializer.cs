using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SA_Online_Mart.Models;

public class DataInitializer
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var logger = serviceProvider.GetRequiredService<ILogger<DataInitializer>>();

        // Seed roles
        await SeedRoleAsync(roleManager, "admin", logger);
        await SeedRoleAsync(roleManager, "customer", logger);

        // Seed Admin
        await SeedAdminUserAsync(userManager, logger);

        // Seed Customers
        await SeedCustomersAsync(userManager, logger);
    }

    private static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager, string roleName, ILogger logger)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            logger.LogInformation($"Creating the {roleName} role.");
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    private static async Task SeedAdminUserAsync(UserManager<AppUser> userManager, ILogger logger)
    {
        var adminEmail = "admin@gmail.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                FirstName = "Admin",
                LastName = "Administrator",
                Address = "Admin 65 RoadRage",
                UserName = adminEmail,
                Email = adminEmail,
                PhoneNumber = "0896745433",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@#1234");

            if (result.Succeeded)
            {
                logger.LogInformation("Admin user created successfully.");
                await userManager.AddToRoleAsync(adminUser, "admin");
            }
            else
            {
                logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors));
            }
        }
    }

    private static async Task SeedCustomersAsync(UserManager<AppUser> userManager, ILogger logger)
    {
        for (int i = 1; i <= 20; i++)
        {
            string email = $"customer{i}@gmail.com";
            var customerUser = await userManager.FindByEmailAsync(email);

            if (customerUser == null)
            {
                customerUser = new AppUser
                {
                    FirstName = $"Customer{i}",
                    LastName = "User",
                    Address = $"Street {i} CustomerVille",
                    UserName = email,
                    Email = email,
                    PhoneNumber = $"08123456{i:D2}",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(customerUser, $"Customer@#{i}123");

                if (result.Succeeded)
                {
                    logger.LogInformation($"Customer {i} created successfully.");
                    var roleResult = await userManager.AddToRoleAsync(customerUser, "customer");

                    if (!roleResult.Succeeded)
                    {
                        logger.LogError($"Failed to assign 'customer' role to {email}: {string.Join(", ", roleResult.Errors)}");
                    }
                }
                else
                {
                    logger.LogError($"Failed to create customer {i}: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }
}