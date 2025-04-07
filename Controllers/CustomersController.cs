using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Data;
using SA_Online_Mart.Services;

namespace SA_Online_Mart.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _customerService.GetAllCustomersAsync();
            return View(users);
        }
    }
}
