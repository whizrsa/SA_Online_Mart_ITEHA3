using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;
using SA_Online_Mart.ViewModel;
using System.Security.Claims;

namespace SA_Online_Mart.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AdminService(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<AdminDashboardViewModel> GetDataCount(ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);

            if (currentUser != null && await _userManager.IsInRoleAsync(currentUser, "admin"))
            {
                var productCount = await _context.Products.CountAsync();
                var customerCount = await _context.Users.CountAsync();
                var categoryCount = await _context.Categories.CountAsync();

                var viewModel = new AdminDashboardViewModel
                {
                    User = currentUser,
                    ProductCount = productCount,
                    CustomerCount = customerCount,
                    CategoryCount = categoryCount
                };

                return viewModel;

            }

            return null;
        }
    }
}
