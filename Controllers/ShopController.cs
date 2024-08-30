using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;

namespace SA_Online_Mart.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }
    }
}
