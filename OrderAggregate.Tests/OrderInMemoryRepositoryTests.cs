using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderAggregate.Core.Interfaces;
using OrderAggregate.Core.Models;
using OrderAggregate.Core.Repositories;
using OrderAggregate.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAggregate.Tests
{
    [TestClass]
    public class OrderInMemoryRepositoryTests
    {
        private readonly OrderInMemoryRepository _orderRepository;

        public OrderInMemoryRepositoryTests()
        {
            _orderRepository = new OrderInMemoryRepository(NullLoggerFactory.Instance.CreateLogger<OrderInMemoryRepository>());
        }

        [TestMethod]
        public void TestAdd()
        {
            Assert.ThrowsException<Exception>(() => _orderRepository.Add(new Order() { ProductID = null, Quantity = 1 }));
            Assert.ThrowsException<Exception>(() => _orderRepository.Add(new Order() { ProductID = "xxx", Quantity = -1 }));
            var order = new Order() { ProductID = "xxx", Quantity = 1 };
            _orderRepository.Add(order);
            Assert.AreEqual(_orderRepository.GetAll().Count(), 1);
            Assert.AreEqual(_orderRepository.GetAll().FirstOrDefault(), order);
        }
    }
}
