using Microsoft.AspNetCore.Mvc;

namespace SA_Online_Mart.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
