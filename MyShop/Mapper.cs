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
            CreateMap< GetUserDTO, User>();
            CreateMap<RegisterUserDTO, User>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<AddOrderDTO, Order>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
