﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Helpers
{
    public abstract class DbConnectionHelper<T>
    {
        public void EnsureConnectionSuccessful()
        {
            if (!TestConnection())
            {
                Console.WriteLine("Connection failed, exit!");
                Environment.Exit(1);
            }

            Console.WriteLine("Connection successful!");
        }

        protected void Write(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(
                       ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected virtual List<T> Read(string queryString)
        {
            throw new NotImplementedException();
        }
        
        protected string ConnectionString;

        public bool TestConnection()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
