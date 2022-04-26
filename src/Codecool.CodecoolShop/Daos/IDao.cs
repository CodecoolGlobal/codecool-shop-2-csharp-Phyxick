using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos
{
    public interface IDao<T> where T : BaseModel
    {
        void Add(T item);
        void Remove(int id);

        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
