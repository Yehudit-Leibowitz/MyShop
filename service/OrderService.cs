using Entity;
using Microsoft.Extensions.Logging;
using NLog;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        IOrderRepository orderRepository;
        IProductRepository productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository , ILogger<OrderService> logger)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await orderRepository.GetOrderById(id);

        }

        public async Task<Order> AddOrder(Order order)
        {
            if (!await CheckSum(order))
            {
                _logger.LogCritical($" The customer with this UserId : {order.UserId} is dangerous");
                return null;
            }
             

            return await orderRepository.AddOrder(order);



        }

        private async Task<bool> CheckSum(Order order)
        {
            List<Product> products = await productRepository.GetProducts(null, null, null, []);
            decimal? amount = 0;
            foreach (var item in order.OrderItems)
            {
                amount += products.Find(product => product.ProductId == item.ProductId).Price;
            }
            return amount == order.OrderSum;
        }

    }
}
