using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoDB : DbConnectionHelper<Supplier>, ISupplierDao
    {
        private List<Supplier> _data = new List<Supplier>();
        private static SupplierDaoDB instance = null;

        private SupplierDaoDB()
        {
        }

        public static SupplierDaoDB GetInstance()
        {
            if (instance == null)
            {
                instance = new SupplierDaoDB();
            }

            return instance;
        }

        public void Add(Supplier item)
        {
            item.Id = _data.Count + 1;
            _data.Add(item);
        }

        public void Remove(int id)
        {
            _data.Remove(this.Get(id));
        }

        public Supplier Get(int id)
        {
            string query = $"SELECT * FROM Supplier" +
                           $"WHERE id = {id}";
            return Read(query).First();
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _data;
        }

        protected override List<Supplier> Read(string queryString)
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
                        {Name = (string) reader["Name"], Description = (string) reader["Description"]};

                    data.Add(supplier);
                }

                return data;
            }
        }
    }
}
