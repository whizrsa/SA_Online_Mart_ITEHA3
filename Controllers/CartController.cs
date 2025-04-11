using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Models;
using SA_Online_Mart.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA_Online_Mart.Controllers
{
    [Authorize(Roles = "customer")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.GetCart(userId);

            if (cart == null)
            {
                cart = new Cart { Items = new List<CartItem>() };
            }

            var total = cart.Items.Sum(i => i.Product.Price * i.Quantity);
            ViewBag.TotalPrice = total;

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.AddToCart(userId, productId, 1);
            return RedirectToAction("Index", "Shop");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.UpdateCartItem(userId, cartItemId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.RemoveFromCart(userId, cartItemId);
            return RedirectToAction("Index");
        }
    }
}
