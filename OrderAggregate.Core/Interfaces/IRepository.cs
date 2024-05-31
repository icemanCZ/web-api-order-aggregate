using OrderAggregate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAggregate.Core.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Add(IEnumerable<T> items);
        IEnumerable<T> GetAll();
    }
}
