using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Http;
using Serilog;


namespace Codecool.CodecoolShop.Controllers
{
    public class PaymentController : Controller
    {
        EmailSender emailSender = new EmailSender();

        private readonly ILogger<PaymentController> _logger;
        public ProductService ProductService { get; set; }
        public UserService UserService { get; set; }

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
            ProductService = ServiceHelper.GetProductService();
            UserService = ServiceHelper.GetUserService();
        }

        public IActionResult ValidateData()
        {
            string username = HttpContext.Session.GetString("username")?.Replace("\"", "");
            bool ValidDate = true;
            if (ValidDate)
            {
                var user = UserService.GetUserData(username);
                user.Name = Request.Form["Name"];
                user.Email = Request.Form["Email"];
                user.Phone = Request.Form["Phone number"];
                user.BillingCountry = Request.Form["Billing Country"];
                user.BillingZipcode = Request.Form["Billing Zipcode"];
                user.BillingCity = Request.Form["Billing City"];
                user.BillingStreet = Request.Form["Billing Street"];
                user.BillingHouseNumber = Request.Form["Billing House"];
                user.ShippingCountry = Request.Form["Shipping Country"];
                user.ShippingZipcode = Request.Form["Shipping Zipcode"];
                user.ShippingCity = Request.Form["Shipping City"];
                user.ShippingStreet = Request.Form["Shipping Street"];
                user.ShippingHouseNumber = Request.Form["Shipping House"];
                UserService.UpdateUserData(user);
                Log.Logger.Information($"Name: {user.Name}");
                Log.Logger.Information($"Email: {user.Email}");
                Log.Logger.Information($"Phone: {user.Phone}");
                Log.Logger.Information($"Country: {user.BillingCountry}");
                Log.Logger.Information($"Zip: {user.BillingZipcode}");
                Log.Logger.Information($"City: {user.BillingCity}");
                Log.Logger.Information($"Street: {user.ShippingStreet}");
                Log.Logger.Information($"House: {user.BillingHouseNumber}");
                Log.Logger.Information($"Country: {user.ShippingCountry}");
                Log.Logger.Information($"Zip: {user.ShippingZipcode}");
                Log.Logger.Information($"City: {user.ShippingCity}");
                Log.Logger.Information($"Street: {user.ShippingStreet}");
                Log.Logger.Information($"House: {user.ShippingHouseNumber}");
                return RedirectToAction("Index", new {name = user.Name,  email = user.Email});
            }
            else
            {
                return RedirectToAction("Checkout", "Cart");
            }
        }

        public IActionResult Index(string name, string email)
        {
            ViewData["name"] = name;
            ViewData["email"] = email;
            return View();
        }

        public IActionResult ValidatePayment()
        {
            bool ValidCardDate = true;
            if (ValidCardDate)
            {
                string username = HttpContext.Session.GetString("username")?.Replace("\"", "");
                var user = UserService.GetUserData(username);
                user.CardHolderName = Request.Form["Card Holder Name"];
                user.CardNumber = Request.Form["Card Number"];
                user.ExpiryDate = Request.Form["Expiry Date"];
                user.CVVCode = Request.Form["CVV"];
                string name = Request.Form["Name"];
                string email = Request.Form["Email"];
                UserService.UpdateUserData(user);
                emailSender.SendConfirmationEmail(name, email, "order");
                return RedirectToAction("Confirmation");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Confirmation()
        {
            Log.CloseAndFlush();
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);
            return View();
        }

    }
}
