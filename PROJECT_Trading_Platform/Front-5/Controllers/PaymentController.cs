using Microsoft.AspNetCore.Mvc;
using front_5.Models;
using Stripe.Checkout;
using Stripe;
using Front_5.Models;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Front_5.Controllers
{
    public class PaymentController : Controller
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;

        }

        [HttpPost]
        public IActionResult CreateCheckoutSession(string amount)
        {

            var currency = "usd"; // Currency code
            var successUrl = "https://localhost:7056/Home/Success";
            var cancelUrl = "https://localhost:7056/Home/Index";
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = Convert.ToInt32(amount) * 1,  // Amount in smallest currency unit (e.g., cents)
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Product Name",
                                Description = "Product Description"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);


            return Redirect(session.Url);
        }

        public async Task<IActionResult> success()
        {

            return View("Index");
        }

        public IActionResult cancel()
        {
            return View("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Privacy()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





    }
}
