using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Serilog;


namespace Codecool.CodecoolShop.Controllers
{
    public class PaymentController : Controller
    {
        EmailSender emailSender = new EmailSender();

        private readonly ILogger<PaymentController> _logger;
        public ProductService ProductService { get; set; }

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());
        }

        public IActionResult ValidateData()
        {
            bool ValidDate = true;
            if (ValidDate)
            {
                string name = Request.Form["Name"];
                string email = Request.Form["Email"];
                string phoneNum = Request.Form["Phone number"];
                string country = Request.Form["Country"];
                string zip = Request.Form["Zipcode"];
                string city = Request.Form["City"];
                string street = Request.Form["Street"];
                string house = Request.Form["House"];
                Log.Logger.Information($"Name: {name}");
                Log.Logger.Information($"Email: {email}");
                Log.Logger.Information($"Phone: {phoneNum}");
                Log.Logger.Information($"Country: {country}");
                Log.Logger.Information($"Zip: {zip}");
                Log.Logger.Information($"City: {city}");
                Log.Logger.Information($"Street: {street}");
                Log.Logger.Information($"House: {house}");
                return RedirectToAction("Index", new {name = name,  email = email});

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
                string name = Request.Form["Name"];
                string email = Request.Form["Email"];
                

                emailSender.SendConfirmationEmail(name, email);
                return RedirectToAction("Confirmation");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Confirmation()
        {
            Log.CloseAndFlush();
            return View();
        }

    }
}
