using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        IOrderService orderService;
        IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            _mapper = mapper;
        }


        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {

            Order foundOrder = await orderService.GetOrderById(id);

            return (foundOrder == null)?
                 NoContent(): Ok(_mapper.Map<Order, OrderDTO>(foundOrder));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddOrderDTO order)
        {
            Order newOrder = await orderService.AddOrder(_mapper.Map<AddOrderDTO, Order>(order));
            return newOrder != null ? Ok(newOrder) : Unauthorized();
        }

    }
}
