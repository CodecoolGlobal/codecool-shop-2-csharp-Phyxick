﻿using System;
using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class CartService
    {
        private readonly ICartDao _cartDao;

        public CartService(ICartDao cartDao)
        {
            _cartDao = cartDao;
        }
        public bool SaveCart(int UserId, List<Item> carts)
        {
            if (_cartDao.SaveShoppingCart(UserId, carts))
                return true;
            return false;
        }

        public List<Item> GetSavedCart(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException("Invalid userId!");
            return _cartDao.GetSavedCart(userId);
        }

        public void SaveOrder(List<Item> cart, int userId)
        {
            _cartDao.SaveOrder(cart, userId);
        }

        public List<Order> GetOrders(int userId)
        {
            return _cartDao.ReadOrderHistory(userId);
        }
    }
}
