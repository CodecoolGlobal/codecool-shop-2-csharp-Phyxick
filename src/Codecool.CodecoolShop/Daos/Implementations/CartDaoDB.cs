using System;
using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos;

public class CartDaoDB : DbConnectionHelper<Item>, ICartDao
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


    public override List<Item> Read(string queryString)
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

    public bool SaveShoppingCart(int UserId, List<Item> cart)
    {
        RemovePrevious(UserId);
        if (UserId != default)
        {
            Guid CartId = Guid.NewGuid();
            foreach (var item in cart)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    string query = @$"INSERT INTO ShoppingCart
                            VALUES ('{item.Product.Id}', '{UserId}');";
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

    public void SaveOrder(List<Item> cart, int userId)
    {
        decimal totalPrice = cart.Sum(i => i.Product.DefaultPrice);
        var totalPriceString = totalPrice.ToString("F");
        var date = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
        string query = $"INSERT INTO OrderHistory VALUES ('{date}', 'Checked', {totalPriceString}, {userId});";
        Write(query);
        var id = GetId();
        foreach (var item in cart)
        {
            string priceString = item.Product.DefaultPrice.ToString("F");
            string itemQuery = $"INSERT INTO OrderHistoryItemList VALUES ('{item.Product.Name}', {priceString}, {item.Quantity}, {id});";
            Write(itemQuery);
        }
    }

    private int GetId()
    {
        string queryString = "SELECT MAX(Id) AS id FROM OrderHistory;";
        using (SqlConnection connection = new SqlConnection(
                   ConnectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();


            return (int)reader["Id"];
        }
    }

    public List<Order> ReadOrderHistory(int userId)
    {
        string queryString = $"SELECT * FROM OrderHistory WHERE User_id = {userId};";
        using (SqlConnection connection = new SqlConnection(
                   ConnectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Order order;
            var orders = new List<Order>();
            while (reader.Read())
            {
                order = new Order
                {
                    Id = (int)reader["Id"],
                    OrderDate = (DateTime)reader["Order_date"],
                    OrderStatus = (string)reader["Order_status"],
                    TotalPrice = (decimal)reader["Total_price"],
                    OrderItems = ReadOrderItems((int)reader["Id"])
                };
                orders.Add(order);

            }


            return orders;
        }
    }


    public List<OrderItem> ReadOrderItems(int id)
    {
        string queryString = $"SELECT * FROM OrderHistoryItemList WHERE Order_history_id = {id};";
        using (SqlConnection connection = new SqlConnection(
                   ConnectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            OrderItem orderItem;
            var items = new List<OrderItem>();
            while (reader.Read())
            {
                orderItem = new OrderItem
                {
                    ItemName = (string)reader["Item_name"],
                    ItemPrice = (decimal)reader["Item_price"],
                    Quantity = (int)reader["Quantity"],
                    OrderHistoryId = (int)reader["Order_history_id"]
                };
                items.Add(orderItem);
            }

            return items;
        }
    }
}