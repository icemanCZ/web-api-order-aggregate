using Microsoft.Extensions.Logging;
using OrderAggregate.Core.Interfaces;
using OrderAggregate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAggregate.Core.Services
{
    public class OrderAggregatorService
    {
        private readonly ILogger<OrderAggregatorService> _logger;
        private readonly IRepository<Order> _orderRepository;

        public OrderAggregatorService(ILogger<OrderAggregatorService> logger, IRepository<Order> orderRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public IEnumerable<AggregatedOrders> GetAggregatedOrders()
        {
            _logger.LogTrace("GetAggregatedOrders");

            return _orderRepository.GetAll()
                .GroupBy(x => x.ProductID)
                .Select(x => new AggregatedOrders(x.Key, x.Sum(x => x.Quantity)));
        }
    }
}
