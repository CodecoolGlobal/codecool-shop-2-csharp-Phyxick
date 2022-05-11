using System.Collections.Generic;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codecool.CodecoolShop.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        public UserService UserService { get; set; }

        EmailSender emailSender = new EmailSender();

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            UserService = new UserService(UserDaoDB.GetInstance());
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
            User user = new User() {Username = username, Password = password};
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
            return RedirectToAction("Index", "Login", new {message = message});
        }

        public IActionResult Register()
        {
            string username = Request.Form["Username"];
            string password = Request.Form["Password"];
            string email = Request.Form["Email"];
            User user = new User() { Id = 1, Username = username, Password = password, Email = email, Name = "", Phone = "", BillingCountry = "", BillingCity = "", BillingZipcode = "", BillingStreet = "", BillingHouseNumber = "", ShippingCountry = "", ShippingCity = "", ShippingZipcode = "", ShippingStreet = "", ShippingHouseNumber = ""};
            if (UserService.Register(user))
            {
                emailSender.SendConfirmationEmail(username, email);
                return RedirectToAction("Index", "Product");
            }
            var message = "User already exists!";
            return (RedirectToAction("Index", "Login", new { message = message }));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Product");
        }
    }
}
