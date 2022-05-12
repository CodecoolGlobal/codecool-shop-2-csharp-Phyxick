using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos
{
    public interface ICartDao
    {
        bool SaveShoppingCart(int UserId, List<Item> carts);

        public List<Item> GetSavedCart(int userId);

        public void SaveOrder( List<Item> cart, int userId);

        public List<Order> ReadOrderHistory(int userId);
    }
}
