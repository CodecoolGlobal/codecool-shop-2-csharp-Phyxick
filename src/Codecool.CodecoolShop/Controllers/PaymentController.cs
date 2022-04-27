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

namespace Codecool.CodecoolShop.Controllers
{
    public class PaymentController : Controller
    {
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
                return RedirectToAction("Index");
            else
            {
                return RedirectToAction("Checkout", "Cart");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ValidatePayment()
        {
            bool ValidCardDate = true;
            if (ValidCardDate)
                return RedirectToAction("Confirmation");
            else
                return RedirectToAction("Index");
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
