using System;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        
        public List<OrderItem> OrderItems { get; set; }
    }
}
