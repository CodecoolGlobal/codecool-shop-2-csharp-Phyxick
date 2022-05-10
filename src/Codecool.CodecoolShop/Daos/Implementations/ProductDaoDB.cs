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
            string query = "INSERT INTO Product (Name, Default_price, Image, Description, Product_category_id, Supplier_id)" +
                           $"Values ('{item.Name}', {item.DefaultPrice}, '{item.Image}', '{item.Description}', {item.ProductCategory.Id}, {item.Supplier.Id});";
            Write(query);
        }

        public void Remove(int id)
        {
            string query = $"DELETE FROM Product WHERE Id = {id}";
            Write(query);
        }

        public Product Get(int id)
        {
            string query = "SELECT * FROM Product" +
                           $"WHERE id = {id}";
            return Read(query).First();
        }

        public IEnumerable<Product> GetAll()
        {
            string query = "SELECT * FROM Product";
            return Read(query);
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            string query = "SELECT * FROM Product" +
                           $"WHERE Supplier_id = {supplier.Id}";
            return Read(query);
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            string query = "SELECT * FROM Product" +
                           $"WHERE Product_category_id = {productCategory.Id}";
            return Read(query);
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory, Supplier supplier)
        {
            string query = "SELECT * FROM Product" +
                           $"WHERE Supplier_id = {supplier.Id} AND Product_category_id = {productCategory.Id}";
            return Read(query);
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
