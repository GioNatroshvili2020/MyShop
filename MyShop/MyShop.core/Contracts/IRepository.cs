using System.Linq;
using MyShop.core.Models;

namespace MyShop.core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    { 
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T item);
        void Update(T item);
    }
}