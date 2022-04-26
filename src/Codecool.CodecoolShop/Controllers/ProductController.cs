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
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());
        }

        public IActionResult Index(string? categories, string? suppliers)
        {
            return categories switch
            {
                null when suppliers is null => View(ProductService.GetProductsForCategory(1).ToList()),
                null => View(GetProductsBySupplier(suppliers).ToList()),
                _ => View(suppliers is null
                    ? GetProductsByCategory(categories).ToList()
                    : GetProductsByCategoryAndSupplier(categories, suppliers).ToList())
            };
        }

        public IEnumerable<Product> GetProductsByCategory(string categories)
        {
            int[] ids = categories.Split(',').Select(int.Parse).ToArray();
            IEnumerable<Product> products = new List<Product>();
            
            foreach (int id in ids)
            {
                products = products.Concat(ProductService.GetProductsForCategory(id));
            }

            return products;
        }

        public IEnumerable<Product> GetProductsBySupplier(string suppliers)
        {
            int[] ids = suppliers.Split(',').Select(int.Parse).ToArray();
            IEnumerable<Product> products = new List<Product>();
            
            foreach (var id in ids)
            {
                products = products.Concat(ProductService.GetProductsForSupplier(id));
            }

            return products;
        }

        public IEnumerable<Product> GetProductsByCategoryAndSupplier(string categories, string suppliers)
        {
            int[] categoryIds = categories.Split(',').Select(int.Parse).ToArray();
            int[] supplierIds = suppliers.Split(',').Select(int.Parse).ToArray();
            
            IEnumerable<Product> products = new List<Product>();
            foreach (var categoryId in categoryIds)
            {
                foreach (var supplierId in supplierIds)
                {
                    products = products.Concat(ProductService.GetProductsForCategoryAndSupplier(categoryId, supplierId));
                }
            }

            return products;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
