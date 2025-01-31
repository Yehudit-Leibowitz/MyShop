using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO

{
    public record GetUserDTO(int UserId ,string UserName, string? FirstName, string? LastName, ICollection<OrderDTO> Orders);
    public record RegisterUserDTO(string UserName, string? FirstName, string? LastName, string Password);
    

}

