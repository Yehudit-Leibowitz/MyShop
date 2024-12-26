using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderDTO(DateTime? OrderDate, int? OrderSum, string UserUserName, ICollection<OrderItem> OrderItems);

    public record AddOrderDTO(DateTime? OrderDate, int? OrderSum, int UserId, ICollection<OrderItem> OrderItems);

}
