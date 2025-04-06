using Microsoft.AspNetCore.Identity;
using SA_Online_Mart.Models;

public class DataInitializer
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        // Seed roles
        if (!await roleManager.RoleExistsAsync("admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }

        if (!await roleManager.RoleExistsAsync("customer"))
        {
            await roleManager.CreateAsync(new IdentityRole("customer"));
        }

        // Seed Admin
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
                await userManager.AddToRoleAsync(adminUser, "admin");
            }
        }

        // Seed 20 Customers
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
                    await userManager.AddToRoleAsync(customerUser, "customer");
                }
            }
        }
    }
}
