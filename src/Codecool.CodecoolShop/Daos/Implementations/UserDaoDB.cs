using System;
using System.Collections.Generic;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDaoDB : DbConnectionHelper<User>, IUserDao
    {
        private static UserDaoDB _instance = null;

        private UserDaoDB()
        {
        }

        public static UserDaoDB GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserDaoDB();
            }

            return _instance;
        }
        public new User Read(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(
                       ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                User user = new User();
                while (reader.Read())
                {
                    user = new User
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Email = (string)reader["Email"],
                        Password = (string)reader["Password"],
                        Username = (string)reader["Username"],
                        Phone = (string)reader["Phone"],
                        BillingCountry = (string)reader["Billing_country"],
                        BillingCity = (string)reader["Billing_city"],
                        BillingZipcode = (string)reader["Billing_zipcode"],
                        BillingStreet = (string)reader["Billing_street"],
                        BillingHouseNumber = (string)reader["Billing_house_number"],
                        ShippingCountry = (string)reader["Shipping_country"],
                        ShippingCity = (string)reader["Shipping_city"],
                        ShippingZipcode = (string)reader["Shipping_zipcode"],
                        ShippingStreet = (string)reader["Shipping_street"],
                        ShippingHouseNumber = (string)reader["Shipping_house_number"]
                    };
                }
                

                return user;
            }
        }

        public bool ValidateLogin(User user)
        {
            string query = $"SELECT * FROM Users WHERE Username = '{user.Username}' AND Password = '{user.Password}';";
            return Read(query).Username == user.Username;
        }

        public void Remove(User user)
        {
            string query = $"DELETE FROM Users WHERE Id = {user.Id};";
            Write(query);
        }

        private bool CheckRegistrationStatus(User user)
        {
            string query = $"SELECT * FROM Users WHERE Username = '{user.Username}';";
            return Read(query).Username == null;
        }

        public bool Register(User user)
        {
            if (CheckRegistrationStatus(user))
            {
                string query = @$"INSERT INTO Users
                            VALUES ('{user.Username}', '{user.Password}', '{user.Name}', 
                            '{user.Email}', '{user.Phone}', '{user.BillingCountry}', 
                            '{user.BillingZipcode}', '{user.BillingCity}', 
                            '{user.BillingStreet}', '{user.BillingHouseNumber}', 
                            '{user.ShippingCountry}', '{user.ShippingZipcode}', 
                            '{user.ShippingCity}', '{user.ShippingStreet}', 
                            '{user.ShippingHouseNumber}', '{user.CardHolderName}', 
                            '{user.CardNumber}', '{user.ExpiryDate}', '{user.CVVCode}');";
                Write(query);
                return true;
            }

            return false;
        }

        public User GetUserData(string username)
        {
            string query = $"SELECT * FROM Users WHERE Username = '{username}';";
            return Read(query);
        }

        public void UpdateUserData(User user)
        {
            string query = @$"UPDATE Users
                            SET Username = '{user.Username}', Password = '{user.Password}', Name = '{user.Name}', 
                            Email = '{user.Email}', Phone = '{user.Phone}', Billing_country = '{user.BillingCountry}', 
                            Billing_zipcode = '{user.BillingZipcode}', Billing_city = '{user.BillingCity}', 
                            Billing_street = '{user.BillingStreet}', Billing_house_number = '{user.BillingHouseNumber}', 
                            Shipping_country = '{user.ShippingCountry}', Shipping_zipcode = '{user.ShippingZipcode}', 
                            Shipping_city = '{user.ShippingCity}', Shipping_street = '{user.ShippingStreet}', 
                            Shipping_house_number = '{user.ShippingHouseNumber}', Card_holder_name = '{user.CardHolderName}', 
                            Card_number = '{user.CardNumber}', Expiry_date = '{user.ExpiryDate}', CVV_code = '{user.CVVCode}'
                            WHERE Id = {user.Id};";
            Write(query);
        }
    }
}
