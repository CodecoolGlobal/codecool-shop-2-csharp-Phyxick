using System;
using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoDB : DbConnectionHelper<ProductCategory>, IProductCategoryDao
    {
        private static ProductCategoryDaoDB _instance = null;

        private ProductCategoryDaoDB()
        {
            sorce = new JsonConfigurationSource();
            sorce.Path = "appsettings.json";
            valami = new JsonConfigurationProvider(sorce);
            string value;
            valami.TryGet("Mode", out value);

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();


            var section = config.GetSection("ConnectionStrings");
            var valami2 =section.GetChildren();
            var valami3 = valami2.First().Value;
            var valami4 = valami2.First(x => x.Key == "DefaultConnection").Value;

        }

        public static ProductCategoryDaoDB GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ProductCategoryDaoDB();
            }

            return _instance;
        }

        public void Add(ProductCategory item)
        {
            string query = $"INSERT INTO ProductCategory (Name, Department, Description) VALUES ('{item.Name}', '{item.Department}', '{item.Description}');";
            Write(query);
        }

        public void Remove(int id)
        {
            string query = $"DELETE FROM ProductCategory WHERE Id = {id};";
            Write(query);
        }

        public ProductCategory Get(int id)
        {
            string query = $"SELECT * FROM ProductCategory WHERE Id = {id}";

            return Read(query).First();
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            string query = "SELECT * FROM ProductCategory";

            return Read(query);
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
                        Id = (int)reader["Id"],
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
