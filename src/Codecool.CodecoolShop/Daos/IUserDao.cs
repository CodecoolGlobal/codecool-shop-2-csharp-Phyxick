using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos
{
    public interface IUserDao
    {
        bool ValidateLogin(User user);

        void Remove(User user);

        bool Register(User user);

        User GetUserData(string username);

        void UpdateUserData(User user);
    }
}
