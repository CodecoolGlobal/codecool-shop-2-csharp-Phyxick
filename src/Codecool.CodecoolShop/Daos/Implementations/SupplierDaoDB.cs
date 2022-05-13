using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoDB : DbConnectionHelper<Supplier>, ISupplierDao
    {
        private static SupplierDaoDB _instance = null;

        private SupplierDaoDB(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public static SupplierDaoDB GetInstance(string connectionString)
        {
            return _instance ??= new SupplierDaoDB(connectionString);
        }

        public void Add(Supplier item)
        {

            string query = $"INSERT INTO Supplier VALUES ({item.Name}, {item.Description});";
            Write(query);
        }

        public void Remove(int id)
        {
            string query = $"DELETE FROM Supplier WHERE Id = {id};";
            Write(query);
        }

        public Supplier Get(int id)
        {
            string query = $"SELECT * FROM Supplier WHERE Id = {id};";
            return Read(query).First();
        }

        public IEnumerable<Supplier> GetAll()
        {
            string query = $"SELECT * FROM Supplier;";
            return Read(query);
        }

        public override List<Supplier> Read(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(
                       ConnectionString))
            {
                List<Supplier> data = new();

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Supplier supplier = new Supplier
                    {
                        Id = (int) reader["Id"], Name = (string) reader["Name"],
                        Description = (string) reader["Description"]
                    };

                    data.Add(supplier);
                }

                return data;
            }
        }
    }
}
