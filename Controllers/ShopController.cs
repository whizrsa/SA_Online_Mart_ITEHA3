using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
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

        public async Task<IActionResult> Index()
        {
            var products = await _shopService.GetAllProductsAsync();

            if (products == null)
            {
                return NotFound("No Products");
            }

            return View(products);
        }
    }
}
