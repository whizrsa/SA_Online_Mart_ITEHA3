using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Services;

namespace SA_Online_Mart.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var products = await _shopService.GetAllProductsAsync(sortOrder, searchString);

            if (products == null)
            {
                return NotFound("No Products");
            }

            return View(products);
        }
    }
}
