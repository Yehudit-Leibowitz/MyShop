using Entity;
using Repository;
using Moq;
using Moq.EntityFrameworkCore;
using service;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tests;



namespace TestProject1
{
    public class UnitOrderTest
    {
        [Fact]
        public async Task CreateOrderSum_CheckOrderSum_ReturnsNull()
        {
            // Arrange
            var orderItems = new List<OrderItem>() { new() { ProductId = 1, Quantity = 1 } };
            var order = new Order { OrderSum = 45, OrderItems = orderItems, UserId = 1 };

            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockProductRepository = new Mock<IProductRepository>();

            var products = new List<Product> { new() { ProductId = 1, Price = 50 } };

            mockProductRepository.Setup(x => x.GetProducts(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?[]>()))
                                 .ReturnsAsync(products);

            mockOrderRepository.Setup(repo => repo.AddOrder(It.IsAny<Order>())).ReturnsAsync((Order)null);

            var mockLogger = new Mock<ILogger<OrderService>>();
            var orderService = new OrderService(mockOrderRepository.Object, mockProductRepository.Object, mockLogger.Object);

            
            // Act
            var result = await orderService.AddOrder(order);

          
            // Assert
            Assert.Null(result);
        
        }
       
        [Fact]
        public async Task CreateOrderSum_CheckOrderSum_ReturnsOrder()
        {
            // Arrange
            var orderItems = new List<OrderItem>() { new() { ProductId = 1, Quantity = 1 } };
            var order = new Order { OrderSum = 50, OrderItems = orderItems, UserId = 1 };

            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.AddOrder(It.IsAny<Order>())).ReturnsAsync(order);



            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProducts(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?[]>()))
                                 .ReturnsAsync(new List<Product>
                                 {
                             new Product { ProductId = 1, Price = 50 }
                                 });

            List<Product> mockProducts = await mockProductRepository.Object.GetProducts(null, null, null, new int?[] { });
         
            foreach (var product in mockProducts ?? new List<Product>())
            {
                Console.WriteLine($"[DEBUG] Product ID: {product.ProductId}, Price: {product.Price}");
            }

            var mockLogger = new Mock<ILogger<OrderService>>();
            var orderService = new OrderService(mockOrderRepository.Object, mockProductRepository.Object, mockLogger.Object);

         
            // Act
            var result = await orderService.AddOrder(order);

            

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.OrderSum, result.OrderSum);
            Assert.Equal(order.UserId, result.UserId);
            Assert.Equal(order.OrderItems.Count, result.OrderItems.Count);
        }


      

    }
}
