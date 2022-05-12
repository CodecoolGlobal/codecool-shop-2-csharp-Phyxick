using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Controllers;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Codecool.CodecoolShop.Controllers
{

    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        public ProductService ProductService { get; set; }

        public UserService UserService { get; set; }

        public CartService CartService { get; set; }

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            ProductService = ServiceHelper.GetProductService();
            UserService = ServiceHelper.GetUserService();
            CartService = ServiceHelper.GetCartService();
        }

        public IActionResult Index()
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);
            }


            return View();
        }

        public IActionResult Buy(string id)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = ProductService.GetProductById(Int32.Parse(id)), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = ProductService.GetProductById(Int32.Parse(id)), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Add(string id)
        {

            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity++;
            }
            else
            {
                cart.Add(new Item { Product = ProductService.GetProductById(Int32.Parse(id)), Quantity = 1 });
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index", "Cart");
        }


        public IActionResult Remove(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (cart[index].Quantity > 1)
            {
                cart[index].Quantity--;
            }
            else
            {
                cart.RemoveAt(index);
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(string idString)
        {
            int id = Int32.Parse(idString);
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult Checkout()
        {
            string username = HttpContext.Session.GetString("username")?.Replace("\"", "");
            ViewData["user"] = UserService.GetUserData(username);
            var Id = Guid.NewGuid();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.File($"Logs\\log-{Id}-.json", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Log.Information("Going to checkout With {@Cart}", cart);
            return View();
        }



        public IActionResult SaveCart()
        {
            string message = "";
            string username = HttpContext.Session.GetString("username")?.Replace("\"", "");
            User user = UserService.GetUserData(username);
            List<Item> carts = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (CartService.SaveCart(user.Id, carts))
            {
                message = "Your Cart is Saved";
                HttpContext.Session.SetString("message", message);
            }
            else
            {
                message = "You must register and log in to save your cart";
                HttpContext.Session.SetString("message", message);

            }
            return RedirectToAction("Index");
        }
    }
}