using Entity;
using Repository;
using Moq;
using Moq.EntityFrameworkCore;
using service;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Logging;



namespace TestProject1
{
    public class UnitOrderTest
    {

        [Fact]
        public async Task Post_ShouldSaveOrder_WithCorrectTotalAmount()
        {
            //Arrange
            var category = new Category { CategoryName = "Electronics" };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var product1 = new Product { ProductName = "Laptop", Price = 10, Image = "laptop.jpg", Category = category };
            var product2 = new Product { ProductName = "Phone", Price = 20, Image = "phone.jpg", Category = category };
            var product3 = new Product { ProductName = "Tablet", Price = 15, Image = "tablet.jpg", Category = category };

            _context.Products.AddRange(product1, product2, product3);
            await _context.SaveChangesAsync();

            var user = new User { UserName = "test@example.com", Password = "password12@@D3", FirstName = "John", LastName = "Doe" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                OrderSum = 0,
                UserId = user.UserId,
                OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = product1.ProductId, Quantity = 1 },
                new OrderItem { ProductId = product2.ProductId, Quantity = 1 },
                new OrderItem { ProductId = product3.ProductId, Quantity = 1 }
            }
            };

            // Act: 
            var savedOrder = await _repository.AddOrder(order);

            // Assert: 
            Assert.Null(savedOrder);



        }

        [Fact]
        public async Task CheckOrderSum_InvalidOrderSumUpdatesOrderSum()
        {
            // Arrange
            var products = new List<Product>
    {
        new Product { ProductId = 1, Price = 40 },
        new Product { ProductId = 2, Price = 20 }
    };

            var invalidOrder = new Order
            {
                UserId = 1,
                OrderSum = 50, // סכום לא נכון
                OrderItems = new List<OrderItem>
        {
            new OrderItem { ProductId = 1, Quantity = 2 },
            new OrderItem { ProductId = 2, Quantity = 1 }
        }
            };

            var mockContext = new Mock<ApiDbToCodeContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            mockContext.Setup(x => x.Orders).ReturnsDbSet(new List<Order>());
            mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.GetProductById(1)).ReturnsAsync(products[0]);
            productRepository.Setup(x => x.GetProductById(2)).ReturnsAsync(products[1]);

            var orderRepository = new OrderRepository(mockContext.Object);
            var mockLogger = new Mock<ILogger<OrderService>>();
            var orderService = new OrderService(orderRepository, productRepository.Object, mockLogger.Object);

            // Act
            var result = await orderService.AddOrder(invalidOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.OrderSum); 
            Assert.Equal(invalidOrder.UserId, result.UserId);
            Assert.Equal(invalidOrder.OrderItems.Count, result.OrderItems.Count);
        }

    }
}
