using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using Microsoft.AspNetCore.Identity;
using SA_Online_Mart.Models;
using Stripe;

namespace SA_Online_Mart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add session services
            builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Configure StripeSettings using values from appsettings.json (Got it from DEV website--for later uses)
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            StripeConfiguration.ApiKey = "sk_test_51PsXyGRtHdFnLFNcD1ODhZnmSOmvjTcYJch6UoFPS2iQHEzJWJpKHNPoT9knnZ1JZ9aA8dCPLyrJzcwdIabVW1Ao00vFehp2lh";


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession(); // Add session middleware here
            app.UseAuthorization();


            // Default routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Admin routes
            app.MapControllerRoute(
                name: "admin",
                pattern: "{controller=Admin}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
