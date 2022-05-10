using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoDB : DbConnectionHelper<ProductCategory>, IProductCategoryDao
    {
        private List<ProductCategory> _data = new List<ProductCategory>();
        private static ProductCategoryDaoDB instance = null;

        private ProductCategoryDaoDB()
        {
        }

        public static ProductCategoryDaoDB GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductCategoryDaoDB();
            }

            return instance;
        }

        public void Add(ProductCategory item)
        {
            item.Id = _data.Count + 1;
            _data.Add(item);
        }

        public void Remove(int id)
        {
            _data.Remove(this.Get(id));
        }

        public ProductCategory Get(int id)
        {
            string query = $"SELECT * FROM ProductCategory" +
                           $"WHERE id = {id}";
            return Read(query).First();
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _data;
        }

        protected override List<ProductCategory> Read(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(
                       ConnectionString))
            {
                List<ProductCategory> data = new();

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductCategory category = new ProductCategory
                    {
                        Name = (string) reader["Name"], Department = (string) reader["Department"],
                        Description = (string) reader["Description"]
                    };

                    data.Add(category);
                }

                return data;
            }
        }
    }
}
