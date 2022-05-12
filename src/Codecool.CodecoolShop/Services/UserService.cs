using System;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class UserService
    {
        private readonly IUserDao _userDao;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public bool ValidateLogin(User user)
        {
            return _userDao.ValidateLogin(user);
        }

        public void Remove(User user)
        {
            _userDao.Remove(user);
        }
        public bool Register(User user)
        {
           return _userDao.Register(user);
        }

        public User GetUserData(string username)
        {
            return _userDao.GetUserData(username);
        }

        public void UpdateUserData(User user)
        {
            _userDao.UpdateUserData(user);
        }
    }
}
