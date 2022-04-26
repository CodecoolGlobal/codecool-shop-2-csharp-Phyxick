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
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            return this.productCategoryDao.GetAll();
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }
        public Product GetProductById(int Id)
        {
            return this.productDao.Get(Id);
        }
        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = this.supplierDao.Get(supplierId);
            return this.productDao.GetBy(supplier);
        }
        public IEnumerable<Product> GetProductsForCategoryAndSupplier(int categoryId, int supplierId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            Supplier supplier = this.supplierDao.Get(supplierId);
            return this.productDao.GetBy(category, supplier);
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return supplierDao.GetAll();
        }
    }
}
