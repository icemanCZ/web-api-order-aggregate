using Microsoft.Extensions.Logging;
using OrderAggregate.Core.Interfaces;
using OrderAggregate.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OrderAggregate.Core.Repositories
{
    public class OrderInMemoryRepository : IRepository<Order>
    {
        private readonly ILogger<OrderInMemoryRepository> _logger;
        private readonly object _locker = new object();
        private readonly List<Order> _data = new List<Order>();

        public OrderInMemoryRepository(ILogger<OrderInMemoryRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Add(Order order)
        {
            lock (_locker)
            {
                if (string.IsNullOrEmpty(order.ProductID))
                    throw new Exception("Order without ProductID is not allowed");

                if (order.Quantity < 0)
                    throw new Exception("Order with negative Quantity is not allowed.");

                _data.Add(order);
            }
        }

        public void Add(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
                Add(order);
        }

        public IEnumerable<Order> GetAll()
        {
            lock (_locker)
            {
                return _data.ToList();
            }
        }

    }
}
