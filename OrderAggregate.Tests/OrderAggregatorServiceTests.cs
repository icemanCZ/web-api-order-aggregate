using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderAggregate.Core.Extensions;
using OrderAggregate.Core.Interfaces;
using OrderAggregate.Core.Models;
using OrderAggregate.Core.Services;
using System.Runtime.CompilerServices;

namespace OrderAggregate.Tests
{
    [TestClass]
    public class OrderAggregatorServiceTests
    {
        private class OrderRepositoryMock : IRepository<Order>
        {
            private List<Order> _orders = new List<Order>();

            public void Add(Order order)
            {
                _orders.Add(order);
            }

            public void Add(IEnumerable<Order> orders)
            {
                _orders.AddRange(orders);
            }

            public IEnumerable<Order> GetAll()
            {
                return _orders;
            }
        }

        private readonly IRepository<Order> _orderRepository;
        private readonly OrderAggregatorService _orderAggregatorService;

        public OrderAggregatorServiceTests()
        {
            _orderRepository = new OrderRepositoryMock();
            _orderAggregatorService = new OrderAggregatorService(NullLoggerFactory.Instance.CreateLogger<OrderAggregatorService>(), _orderRepository);
        }

        [TestMethod]
        public void TestAggregate()
        {
            _orderRepository.Add(new Order() { ProductID = "a", Quantity = 1 });
            _orderRepository.Add(new Order() { ProductID = "a", Quantity = 1 });
            _orderRepository.Add(new Order() { ProductID = "b", Quantity = 1 });
            _orderRepository.Add(new Order() { ProductID = "c", Quantity = 1 });

            Assert.IsNotNull(_orderAggregatorService.GetAggregatedOrders());
            Assert.IsTrue(_orderAggregatorService.GetAggregatedOrders().Count() == 3);
            Assert.IsTrue(_orderAggregatorService.GetAggregatedOrders().First().Quantity == 2);
        }
    }
}