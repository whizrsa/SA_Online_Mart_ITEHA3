using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Data;

namespace SA_Online_Mart.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users;
            return View(users);
        }
    }
}
