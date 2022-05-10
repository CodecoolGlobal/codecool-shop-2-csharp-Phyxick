using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoDB : IProductDao
    {
        private List<Product> data = new List<Product>();
        private static ProductDaoDB instance = null;

        private ProductDaoDB()
        {
        }

        public static ProductDaoDB GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductDaoDB();
            }

            return instance;
        }

        public void Add(Product item)
        {
            item.Id = data.Count + 1;
            data.Add(item);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public Product Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return data;
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            return data.Where(x => x.Supplier.Id == supplier.Id);
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            return data.Where(x => x.ProductCategory.Id == productCategory.Id);
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory, Supplier supplier)
        {
            return data.Where(x => x.Supplier.Id == supplier.Id && x.ProductCategory.Id == productCategory.Id);
        }
    }
}
