using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codecool.CodecoolShop.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        public UserService UserService { get; set; }

        EmailSender emailSender = new EmailSender();

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            UserService = ServiceHelper.GetUserService();
        }

        public IActionResult Index(string? message)
        {
            ViewData["message"] = message;
            return View();
        }

        public IActionResult ValidateLogin()
        {
            string username = Request.Form["Username"];
            string password = Request.Form["Password"];
            User user = new User() { Username = username, Password = password };
            if (UserService.ValidateLogin(user))
            {
                if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "username") == null)
                {
                    List<Item> cart = new List<Item>();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "username", username);
                }
                return RedirectToAction("Index", "Product");
            }

            var message = "Please enter the correct credentials!";
            return RedirectToAction("Index", "User", new { message = message });
        }

        public IActionResult Register()
        {
            string username = Request.Form["Username"];
            string password = Request.Form["Password"];
            string email = Request.Form["Email"];
            User user = new User() { Username = username, Password = password, Email = email, Name = "", Phone = "", BillingCountry = "", BillingCity = "", BillingZipcode = "", BillingStreet = "", BillingHouseNumber = "", ShippingCountry = "", ShippingCity = "", ShippingZipcode = "", ShippingStreet = "", ShippingHouseNumber = "", CardHolderName = "", CardNumber = "", ExpiryDate = "", CVVCode = "" };
            if (UserService.Register(user))
            {
                emailSender.SendConfirmationEmail(username, email);
                return RedirectToAction("Index", "Product");
            }
            var message = "User already exists!";
            return (RedirectToAction("Index", "User", new { message = message }));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Details()
        {
            string username = HttpContext.Session.GetString("username")?.Replace("\"", "");
            ViewData["user"] = UserService.GetUserData(username);
            return View();
        }

        public IActionResult UpdateUserData()
        {
            string username = HttpContext.Session.GetString("username")?.Replace("\"", "");
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
            ViewData["user"] = UserService.GetUserData(username);
            return RedirectToAction("Details");
        }
    }
}
