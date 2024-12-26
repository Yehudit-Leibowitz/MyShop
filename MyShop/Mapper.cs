using AutoMapper;
using DTO;
using Entity;

namespace MyShop

{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, GetUserDTO>();
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap< OrderItemDTO , OrderItem>();
            CreateMap< GetUserDTO, User>();
            CreateMap<RegisterUserDTO, User>();
            CreateMap<User , RegisterUserDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<AddOrderDTO, Order>();
            CreateMap<OrderDTO,Order > ();
            CreateMap<Order,AddOrderDTO>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
