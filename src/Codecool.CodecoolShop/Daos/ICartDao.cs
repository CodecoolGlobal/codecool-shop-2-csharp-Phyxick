using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos
{
    public interface ICartDao
    {
        bool SaveShoppingCart(int UserId, List<Item> carts);

        public List<Item> GetSavedCart(int userId);
    }
}
