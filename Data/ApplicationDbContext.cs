using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var admin = new IdentityRole("admin")
            {
                Id = "a1f2b3c4-d5e6-47f8-9a0b-c1d2e3f4a5b6",
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            var customer = new IdentityRole("customer")
            {
                Id = "b6a5f4e3-d2c1-0b9a-8f7e-6d5c4b3f2a1f",
                Name = "customer",
                NormalizedName = "CUSTOMER"
            };


            modelBuilder.Entity<IdentityRole>().HasData(admin, customer);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

        }
    }
}
