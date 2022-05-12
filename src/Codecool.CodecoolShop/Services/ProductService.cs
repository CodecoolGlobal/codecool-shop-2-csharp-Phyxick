using System;
using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao supplierDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.supplierDao = supplierDao;
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            var category = this.productCategoryDao.Get(categoryId);
            return category ?? throw new ArgumentException("Invalid Id!");
        }
        public Supplier GetSupplier(int supplierId)
        {
            var supplier = this.supplierDao.Get(supplierId);
            return supplier ?? throw new ArgumentException("Invalid Id!");
        }

        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            return this.productCategoryDao.GetAll();
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            if (category == null)
                throw new ArgumentException("Invalid Id!");
            return this.productDao.GetBy(category);
        }
        public Product GetProductById(int Id)
        {
            var product = this.productDao.Get(Id);
            return product ?? throw new ArgumentException("Invalid Id!");
        }
        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = this.supplierDao.Get(supplierId);
            if (supplier == null)
                throw new ArgumentException("Invalid Id!");
            return this.productDao.GetBy(supplier);
        }
        public IEnumerable<Product> GetProductsForCategoryAndSupplier(int categoryId, int supplierId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            Supplier supplier = this.supplierDao.Get(supplierId);
            if (supplier == null || category == null)
                throw new ArgumentException("Invalid Id!");
            return this.productDao.GetBy(category, supplier);
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return supplierDao.GetAll();
        }
    }
}
