using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {

        ApiDbToCodeContext _apiDbToCodeContext;
        public OrderRepository(ApiDbToCodeContext ApiDbToCodeContext)
        {
            _apiDbToCodeContext = ApiDbToCodeContext;

        }



        public async Task<Order> GetOrderById(int id)
        {
           
            return await _apiDbToCodeContext.Orders.Include(order => order.User).Include(order => order.OrderItems).FirstOrDefaultAsync(order => order.OrderId == id);
 

        }

        public async Task<Order> AddOrder(Order order)
        {
            await _apiDbToCodeContext.Orders.AddAsync(order);
            await _apiDbToCodeContext.SaveChangesAsync();

            return order;

        }

    }
}
