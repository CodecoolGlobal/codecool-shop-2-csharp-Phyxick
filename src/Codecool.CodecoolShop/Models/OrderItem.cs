namespace Codecool.CodecoolShop.Models;

public class OrderItem
{
    public string ItemName { get; set; }
    public decimal ItemPrice { get; set; }
    public int Quantity { get; set; }
    public int OrderHistoryId { get; set; }
}