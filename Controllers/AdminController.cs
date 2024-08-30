using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;
using SA_Online_Mart.ViewModel;
using System.Threading.Tasks;

namespace SA_Online_Mart.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && await _userManager.IsInRoleAsync(currentUser, "admin"))
            {
                var productCount = await _context.Products.CountAsync();
                var customerCount = await _context.Users.CountAsync();

                var viewModel = new AdminDashboardViewModel
                {
                    User = currentUser,
                    ProductCount = productCount,
                    CustomerCount = customerCount
                };

                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
