using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Models;
using Stripe;

namespace SA_Online_Mart.Controllers
{
    public class CheckoutController : Controller
    {
        [TempData]
        public string TotalAmount { get; set; }

        //Make Sure the the "Cart" is the same for all sessions. It is case sensitive
        public IActionResult Index()
        {
            var cart = SessionExtensions.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");
            ViewBag.cart = cart;

            // Calculating total amount
            var totalDollarAmount = cart.Sum(item => item.Product.Price * item.Quantity);
            ViewBag.DollarAmount = totalDollarAmount;

            // Converting totalDollarAmount to the smallest currency unit (e.g., cents)
            long totalAmountInCents = Convert.ToInt64(Math.Round(totalDollarAmount, 2) * 100);
            ViewBag.Total = totalAmountInCents;

            // Storing the total amount in TempData for the next request
            TempData["TotalAmount"] = totalAmountInCents.ToString();

            return View();
        }


        [HttpPost]
        public IActionResult Processing(string stripeToken, string stripeEmail)
        {
            // Create customer
            //Future Updates could be allowing a logged in users details to be added to the stripe built in function
            var optionsCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = "Mr/Mrs",
                Phone = "04-234567"
            };

            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionsCust);

            // Retrieve the total amount from TempData
            long totalAmount = Convert.ToInt64(TempData["TotalAmount"]);

            // Create charge
            var optionsCharge = new ChargeCreateOptions
            {
                Amount = totalAmount,
                Currency = "NZD",
                Description = "Buying Products",
                Source = stripeToken,
                ReceiptEmail = stripeEmail
            };

            var serviceCharge = new ChargeService();

            Charge charge = serviceCharge.Create(optionsCharge);

            if (charge.Status == "succeeded")
            {
                ViewBag.AmountPaid = Convert.ToDecimal(charge.Amount) / 100;
                ViewBag.Customer = customer.Name;
            }

            return View();
        }
    }
}
