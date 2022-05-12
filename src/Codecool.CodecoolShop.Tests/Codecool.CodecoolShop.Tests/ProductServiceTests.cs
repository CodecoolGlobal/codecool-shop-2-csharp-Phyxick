using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Codecool.CodecoolShop;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace Codecool.CodecoolShop.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IProductCategoryDao _category;
        private ISupplierDao _supplier;
        private IProductDao _product;
        private ProductService _productService;
        [SetUp]
        public void Setup()
        {
            _product = Substitute.For<IProductDao>();
            _supplier = Substitute.For<ISupplierDao>();
            _category = Substitute.For<IProductCategoryDao>();
            _productService = new ProductService(_product, _category, _supplier);
        }

        [Test]
        public void GetCategoryValidValue()
        {
            ProductCategory category = new ProductCategory() { Id = 1, Name = "WorkPlease1" };
            _category.Get(1).Returns(category);
            Assert.AreEqual(category, _productService.GetProductCategory(1));
        }

        [Test]
        public void GetCategoryInvalidValue()
        {
            _category.Get(1).ReturnsNull();
            Assert.Throws<ArgumentException>(() => { _productService.GetProductCategory(1); });
        }

        [Test]
        public void GetSupplierInvalidValue()
        {
            _supplier.Get(1).ReturnsNull();
            Assert.Throws<ArgumentException>(() => { _productService.GetSupplier(1); });
        }
        [Test]
        public void GetSupplierValidValue()
        {
            Supplier supplier = new Supplier() { Id = 1, Name = "WorkPlease1" };
            _supplier.Get(1).Returns(supplier);
            Assert.AreEqual(supplier, _productService.GetSupplier(1));
        }

        [Test]
        public void GetAllProductCategoriesValidValue()
        {
            IEnumerable<ProductCategory> categories = new List<ProductCategory>
            {
                new ProductCategory {Id = 1, Name = "PleaseWork1"},
                new ProductCategory {Id = 2, Name = "PleaseWork2"},
                new ProductCategory {Id = 3, Name = "PleaseWork3"}
            };
            _category.GetAll().Returns(categories);
            Assert.AreEqual(categories, _productService.GetAllProductCategories());
        }

        [Test]
        public void GetAllProductCategoriesInvalidValue()
        {
            _category.GetAll().ReturnsNull();
            Assert.IsNull(_productService.GetAllProductCategories());
        }
        
        [Test]
        public void GetProductForCertainCategoryInvalidValue()
        {
            _category.Get(1).ReturnsNull();
            Assert.Throws<ArgumentException>(() => { _productService.GetProductsForCategory(1); });
        }

        [Test]
        public void GetProductForCertainCategoryValidValue()
        {
            var validCategory = new ProductCategory {Id = 1, Name = "WorkPlease"};
            _category.Get(1).Returns(validCategory);
            IEnumerable<Product> products = new List<Product>
            {
                new Product {Id = 1, Name = "PleaseWork1"},
                new Product {Id = 2, Name = "PleaseWork2"},
                new Product {Id = 3, Name = "PleaseWork3"}
            };
            _product.GetBy(validCategory).Returns(products);
            Assert.AreEqual(products, _productService.GetProductsForCategory(1));
        }
        
        [Test]
        public void GetProductByIdInvalidValue()
        {
            _product.Get(1).ReturnsNull();
            Assert.Throws<ArgumentException>(() => _productService.GetProductById(1));
        }

        [Test]
        public void GetProductByIdValidValue()
        {
            var testProduct = new Product {Id = 1, Name = "WorkPlease1"};
            _product.Get(1).Returns(testProduct);
            Assert.AreEqual(testProduct, _productService.GetProductById(1));
        }

        [Test]
        public void GetProductByIdWhileServerMalfunction()
        {
            _product.Get(1).Throws(new IOException());
            Assert.Throws<IOException>(() => _productService.GetProductById(1));
        }

        [Test]
        public void GetFilteredBySupplierValidValue()
        {
            IEnumerable<Product> products = new List<Product>
            {
                new Product {Id = 1, Name = "PleaseWork1"},
                new Product {Id = 2, Name = "WorkPlease2"}
            };
            Supplier supplier = new Supplier {Id = 1, Name = "WorkPlease1"};
            _supplier.Get(1).Returns(supplier);
            _product.GetBy(supplier).Returns(products);
            Assert.AreEqual(products, _productService.GetProductsForSupplier(1));
        }
        

        [Test]
        public void GetFilteredBySupplierInvalidValue()
        {
            _supplier.Get(1).ReturnsNull();
            Assert.Throws<ArgumentException>(() => { _productService.GetProductsForSupplier(1); });
        }

        [Test]
        public void GetFilteredByCategoryAndSupplierValidValue()
        {
            IEnumerable<Product> products = new List<Product>()
            {
                new Product() {Id = 1, Name = "PleaseWork1"},
                new Product() {Id = 2, Name = "WorkPlease2"}
            };
            ProductCategory category = new ProductCategory() { Id = 1, Name = "WorkPlease1" };
            Supplier supplier = new Supplier() { Id = 1, Name = "WorkPlease1" };
            _category.Get(1).Returns(category);
            _supplier.Get(1).Returns(supplier);
            _product.GetBy(category, supplier).Returns(products);
            Assert.AreEqual(products, _productService.GetProductsForCategoryAndSupplier(1, 1));
        }

        [Test]
        public void GetFilteredByCategoryAndSupplierInvalidValue()
        {
            _category.Get(1).ReturnsNull();
            _supplier.Get(1).ReturnsNull();
            Assert.Throws<ArgumentException>(() => { _productService.GetProductsForCategoryAndSupplier(1, 1); });
        }

        [Test]
        public void GetAllSuppliersValidValue()
        {
            IEnumerable<Supplier> suppliers = new List<Supplier>
            {
                new Supplier {Id = 1, Name = "PleaseWork1"},
                new Supplier {Id = 2, Name = "PleaseWork2"},
                new Supplier {Id = 3, Name = "PleaseWork3"}
            };
            _supplier.GetAll().Returns(suppliers);
            Assert.AreEqual(suppliers, _productService.GetAllSuppliers());
        }

        [Test]
        public void GetAllSuppliersInvalidValue()
        {
            _supplier.GetAll().ReturnsNull();
            Assert.IsNull(_productService.GetAllSuppliers());
        }
    }
}