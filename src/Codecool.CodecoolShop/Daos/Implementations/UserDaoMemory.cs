using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDaoMemory : IUserDao
    {
        private static UserDaoMemory _instance = null;

        private UserDaoMemory()
        {
        }

        public static UserDaoMemory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserDaoMemory();
            }

            return _instance;
        }
        public bool ValidateLogin(User user)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool Register(User user)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserData(string username)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUserData(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
