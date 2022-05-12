using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos;

public class CartDaoDB : DbConnectionHelper<User>, ICartDao
{
    private static CartDaoDB _instance = null;

    private CartDaoDB(string connectionString)
    {
        ConnectionString = connectionString;
    }

    private void RemovePrevious(int userId)
    {
        string query = $"DELETE FROM ShoppingCart WHERE User_id = {userId};";
        Write(query);
    }


    public List<Item> Read(string queryString)
    {
        using (SqlConnection connection = new SqlConnection(
                   ConnectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Item item;
            List<Item> cart = new List<Item>();
            while (reader.Read())
            {
                item = new Item
                {
                    Product = new Product
                    {
                        Id = (int)reader["Product_id"],
                        Name = (string)reader["Name"],
                        DefaultPrice = (decimal)reader["Default_price"],
                        Image = (string)reader["Image"],
                        Description = (string)reader["Description"],
                        ProductCategory = new ProductCategory(),
                        Supplier = new Supplier(),

                    },
                    Quantity = (int)reader["Quantity"]
                };
                cart.Add(item);
            }
            // ShoppingCart.Product_id, ShoppingCart.User_id, Product.Name, Product.Default_price, Product.Image, Product.Description, Product.Product_category_id, Product.Supplier_id, Count(Product.Id) as Quantity

            return cart;
        }
    }
    public static ICartDao GetInstance(string connectionString)
    {
        if (_instance == null)
        {
            _instance = new CartDaoDB(connectionString);
        }

        return _instance;
    }

    public bool SaveShoppingCart(int UserId, List<Item> carts)
    {
        RemovePrevious(UserId);
        if (UserId != default)
        {
            Guid CartId = Guid.NewGuid();
            foreach (var cart in carts)
            {
                for (int i = 0; i < cart.Quantity; i++)
                {
                    string query = @$"INSERT INTO ShoppingCart
                            VALUES ('{cart.Product.Id}', '{UserId}');";
                    Write(query);
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Item> GetSavedCart(int userId)
    {
        List<Item> cart = new List<Item>();
        string query = @$"SELECT ShoppingCart.Product_id, ShoppingCart.User_id, Product.Name, Product.Default_price, Product.Image, Product.Description, Product.Product_category_id, Product.Supplier_id, Count(Product.Id) as Quantity
        FROM ShoppingCart
        LEFT JOIN Product
            ON ShoppingCart.Product_id = Product.Id
        WHERE ShoppingCart.User_id = '{userId}'
        GROUP BY ShoppingCart.Product_id, ShoppingCart.User_id, Product.Id, Product.Name, Product.Default_price, Product.Image, Product.Description, Product.Product_category_id, Product.Supplier_id;";
        cart = Read(query);
        return cart;
    }
}