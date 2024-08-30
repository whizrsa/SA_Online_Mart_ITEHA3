using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;
using System.Diagnostics;

namespace SA_Online_Mart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Check if the user is authenticated and is an admin
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (await _userManager.IsInRoleAsync(user, "admin"))
                {
                    // Redirect admin users to the Admin Index view
                    return RedirectToAction("Index", "Admin", new { area = "" });
                }
            }

            // Get the latest 6 products ordered by DateAdded descending
            var latestProducts = _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.DateAdded)
                .Take(6)
                .ToList();

            return View(latestProducts);
        }

        public IActionResult Aboutus()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            // Find the product by its ID, including the Category information
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            // If no product is found, return a NotFound view or an error message
            if (product == null)
            {
                return NotFound();
            }

            // Pass the product to the view
            return View(product);
        }
    }
}
