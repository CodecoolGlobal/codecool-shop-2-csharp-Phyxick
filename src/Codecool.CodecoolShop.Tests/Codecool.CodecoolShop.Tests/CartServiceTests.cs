using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using NSubstitute;
using NUnit.Framework;

namespace Codecool.CodecoolShop.Tests
{
    [TestFixture]
    public class CartServiceTests
    {
        private ICartDao _cartDao;
        private CartService _cartService;
        private List<Item> _carts;

        [SetUp]
        public void Setup()
        {
            _cartDao = Substitute.For<ICartDao>();
            _cartService = new CartService(_cartDao);
            _carts = new List<Item>
            {
                new Item() {Product = new Product(), Quantity = 1},
                new Item() {Product = new Product(), Quantity = 2}
            };
        }

        [Test]
        public void SaveCartValidValues()
        {
            _cartDao.SaveShoppingCart(1, _carts).Returns(true);
            Assert.IsTrue(_cartService.SaveCart(1, _carts));
        }

        [Test]
        public void SaveCartInvalidValues()
        {
            _cartDao.SaveShoppingCart(0, _carts).Returns(false);
            Assert.IsFalse(_cartService.SaveCart(0, _carts));
        }

        [Test]
        public void GetSavedCartValidValues()
        {
            _cartDao.GetSavedCart(1).Returns(_carts);
            Assert.AreEqual(_carts, _cartService.GetSavedCart(1));
        }

        [Test]
        public void GetSavedCartInvalidValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _cartService.GetSavedCart(0));
        }
    }
}
