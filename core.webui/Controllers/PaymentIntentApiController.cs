using System;
using System.Linq;
using core.business.Abstract;
using core.webui.Identity;
using core.webui.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Stripe;

namespace core.webui.Controllers
{
    [Route("api")]
    [ApiController]
    public class PaymentIntentApiController : Controller
    {

        private ICartService _cartService;
        private UserManager<User> _userManager;
        public PaymentIntentApiController(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            string userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCartByUserId(userId);

            // Alternatively, set up a webhook to listen for the payment_intent.succeeded event
            // and attach the PaymentMethod to a new Customer
            var customers = new CustomerService();
            var customer = customers.Create(new CustomerCreateOptions());

            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Customer = customer.Id,
                SetupFutureUsage = "off_session",
                Amount = (long)CalculateOrderAmount(_userManager.GetUserId(User)),
                Currency = "try",
                Shipping = new ChargeShippingOptions
                {
                    Name = request.Items[0].FirstName + " " + request.Items[0].LastName,
                    Phone = request.Items[0].Phone,
                    Address = new AddressOptions
                    {
                        City = request.Items[0].City,
                        Line1 = request.Items[0].Address,
                    }
                }
            });

            return Json(new
            {
                clientSecret = paymentIntent.ClientSecret
            });
        }

        private decimal CalculateOrderAmount(string userId)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client

            return (_cartService.CalculateTotal(userId)*100);
        }

        public void ChargeCustomer(string customerId)
        {
            // Lookup the payment methods available for the customer
            var paymentMethods = new PaymentMethodService();
            var availableMethods = paymentMethods.List(new PaymentMethodListOptions
            {
                Customer = customerId,
                Type = "card",
            });

            // Charge the customer and payment method immediately
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = 1099,
                Currency = "usd",
                Customer = customerId,
                PaymentMethod = availableMethods.Data[0].Id,
                OffSession = true,
                Confirm = true
            });

            if (paymentIntent.Status == "succeeded")
                Console.WriteLine("✅ Successfully charged card off session");
        }

        public class Item
        {
            [JsonProperty("firstName")]
            public string FirstName { get; set; }
            [JsonProperty("lastName")]
            public string LastName { get; set; }
            [JsonProperty("city")]
            public string City { get; set; }
            [JsonProperty("address")]
            public string Address { get; set; }
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("phone")]
            public string Phone { get; set; }

        }

        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }
    }
}
