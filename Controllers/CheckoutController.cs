using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Models;
using SA_Online_Mart.Services;
using Stripe;
using System.Security.Claims;

namespace SA_Online_Mart.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        public CheckoutController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "User is not authenticated.";
                return RedirectToAction("Index", "Home");
            }

            var cart = await _cartService.GetCart(userId);
            if (cart == null || !cart.Items.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession(string stripeToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "User is not authenticated.";
                return RedirectToAction("Index", "Home");
            }

            var cart = await _cartService.GetCart(userId);
            if (cart == null || !cart.Items.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }
            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = (long)cart.Items.Sum(item => item.Product.Price * item.Quantity * 100),
                    Currency = "zar",
                    Description = "Online Mart Purchase",
                    Source = stripeToken,
                };

                var service = new ChargeService();
                var charge = service.Create(options);

                if (charge.Status == "succeeded")
                {
                    // Clear the cart after successful payment
                    await _cartService.ClearCart(userId);
                    TempData["Success"] = "Payment successful!";
                    return RedirectToAction("Success");
                }
                else
                {
                    TempData["Error"] = "Payment failed. Please try again.";
                    return RedirectToAction("Index", "Cart");
                }
            }
            catch (StripeException ex)
            {
                TempData["Error"] = $"Payment failed: {ex.Message}";
                return RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index", "Cart");
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
