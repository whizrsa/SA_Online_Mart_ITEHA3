using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Data;
using SA_Online_Mart.Services;

namespace SA_Online_Mart.Controllers
{
    [Authorize(Roles = "admin")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var customerList = await _customerService.GetAllCustomers(searchString);

            ViewData["CurrentFilter"] = searchString;
            return View(customerList);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var customerMember = await _customerService.Delete(id);
            if (customerMember == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
