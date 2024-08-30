using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace SA_Online_Mart.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.Find(productId);
            var cart = SessionExtensions.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");

            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var cartItem = cart.FirstOrDefault(i => i.Product.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }

            SessionExtensions.SetObjectAsJson(HttpContext.Session, "Cart", cart);

            return RedirectToAction("Index", "Shop");
        }

        [HttpPost]
        public IActionResult UpdateCartItem(int cartItemId, int quantity)
        {
            var cart = SessionExtensions.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");
            if (cart == null)
            {
                return NotFound();
            }

            var cartItem = cart.FirstOrDefault(i => i.CartItemId == cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
            }
            else
            {
                return NotFound();
            }

            SessionExtensions.SetObjectAsJson(HttpContext.Session, "Cart", cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var cart = SessionExtensions.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");
            if (cart == null)
            {
                return NotFound();
            }

            var cartItem = cart.FirstOrDefault(i => i.CartItemId == cartItemId);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
            }

            SessionExtensions.SetObjectAsJson(HttpContext.Session, "Cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cartItems = SessionExtensions.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");

            if (cartItems == null)
            {
                cartItems = new List<CartItem>();  // Initialize with an empty list
            }

            var cart = new Cart { Items = cartItems };  // Wrap items in a Cart object
            return View(cart);
        }
    }
}
