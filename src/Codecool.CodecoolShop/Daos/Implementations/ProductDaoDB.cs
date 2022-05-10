using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoDB : DbConnectionHelper<Product>, IProductDao
    {
        private readonly List<Product> _data = new();
        private static ProductDaoDB _instance;
        private readonly ProductService _productService;

        private ProductDaoDB()
        {
            _productService = new ProductService(GetInstance(), ProductCategoryDaoDB.GetInstance(),
                SupplierDaoDB.GetInstance());
        }

        public static ProductDaoDB GetInstance()
        {
            return _instance ??= new ProductDaoDB();
        }

        public void Add(Product item)
        {
            item.Id = _data.Count + 1;
            _data.Add(item);
        }

        public void Remove(int id)
        {
            _data.Remove(this.Get(id));
        }

        public Product Get(int id)
        {
            return _data.Find(x => x.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _data;
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            return _data.Where(x => x.Supplier.Id == supplier.Id);
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            return _data.Where(x => x.ProductCategory.Id == productCategory.Id);
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory, Supplier supplier)
        {
            return _data.Where(x => x.Supplier.Id == supplier.Id && x.ProductCategory.Id == productCategory.Id);
        }

        protected override List<Product> Read(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(
                       ConnectionString))
            {
                List<Product> data = new();

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product
                    {
                        Name = (string) reader["Name"], DefaultPrice = (decimal) reader["Default_price"],
                        Image = (string) reader["Image"], Description = (string) reader["Description"],
                        ProductCategory = _productService.GetProductCategory((int) reader["Product_category_id"]),
                        Supplier = _productService.GetSupplier((int) reader["Supplier_id"])
                    };
                    
                    data.Add(product);
                }

                return data;
            }
        }
    }
}
