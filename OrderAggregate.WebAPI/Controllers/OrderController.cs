using Microsoft.AspNetCore.Mvc;
using OrderAggregate.Core.Interfaces;
using OrderAggregate.Core.Models;

namespace OrderAggregate.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IRepository<Order> _orderRepository;

        public OrderController(ILogger<OrderController> logger, IRepository<Order> orderRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        [HttpPut]
        public void Put(IEnumerable<Order> orders)
        {
            _logger.LogTrace("WriteOrders");
            _orderRepository.Add(orders);
        }
    }
}
