using Microsoft.AspNetCore.Identity;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.Data
{
    // I created a class that adds a default admin
    public class DataInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            // Does role exists or not
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            // Does emaileExist or not
            var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    FirstName = "Admin",
                    LastName = "Administrator",
                    Address = "Admin 65 RoadRage",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    PhoneNumber = "0896745433",
                    EmailConfirmed = false
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@#1234"); // Set a secure password here

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "admin");
                }
            }
        }
    }
}
