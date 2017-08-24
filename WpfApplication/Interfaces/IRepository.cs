using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>where T:class
    {
        IEnumerable<T> GetAll();

        T Get(int id);
        IEnumerable<T> Find(Predicate<T> predicate);

        void Add(T item);
        void Update(T item);

        void Remove(T item);
        void Remove(int id);
    }
}
