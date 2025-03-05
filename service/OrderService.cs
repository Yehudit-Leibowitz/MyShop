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

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, ILogger<OrderService> logger)
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

        //private async Task<bool> CheckSum(Order order)

        //{
        //    if (order == null)
        //    {
        //        Console.WriteLine("Error: Order is NULL");
        //        return false;
        //    }

        //    if (order.OrderItems == null || !order.OrderItems.Any())
        //    {
        //        Console.WriteLine("Error: OrderItems is NULL or EMPTY");
        //        return false;
        //    }

        //    if (order.OrderItems.FirstOrDefault()?.ProductId == null)
        //    {
        //        Console.WriteLine("Error: ProductId in OrderItems is NULL");
        //        return false;
        //    }





        //    List<Product> products = await productRepository.GetProducts(null, null, null, new int?[] { });
        //    decimal? amount = 0;
        //    foreach (var item in order.OrderItems)
        //    {
        //        amount += products.Find(product => product.ProductId == item.ProductId).Price;
        //    }
        //    return amount == order.OrderSum;
        //}
        private async Task<bool> CheckSum(Order order)
        {
            if (order == null)
            {
                Console.WriteLine("Error: Order is NULL");
                return false;
            }

            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                Console.WriteLine("Error: OrderItems is NULL or EMPTY");
                return false;
            }

            if (order.OrderItems.FirstOrDefault()?.ProductId == null)
            {
                Console.WriteLine("Error: ProductId in OrderItems is NULL");
                return false;
            }

            Console.WriteLine("Fetching products from repository...");
            List<Product> products = await productRepository.GetProducts(null, null, null, new int?[] { });

            if (products == null || !products.Any())
            {
                Console.WriteLine("Error: GetProducts returned NULL or EMPTY list");
                return false;
            }

            decimal? amount = 0;

            foreach (var item in order.OrderItems)
            {
                var product = products.Find(product => product.ProductId == item.ProductId);
                if (product == null)
                {
                    Console.WriteLine($"Error: Product with ID {item.ProductId} not found in GetProducts");
                    return false;
                }
                Console.WriteLine($"Adding Product ID: {product.ProductId}, Price: {product.Price} to amount");
                amount += product.Price;
            }

            Console.WriteLine($"[DEBUG] Final Amount Calculated: {amount}, Expected OrderSum: {order.OrderSum}");
            return amount == order.OrderSum;
        }
    }
}