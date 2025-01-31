using Microsoft.AspNetCore.Mvc;

using service;
using Entity;
using AutoMapper;
using DTO;
using System.Collections.Generic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase

    {
        IProductService poductService;

        IMapper _mapper;
        public ProductsController(IProductService poductService , IMapper mapper)
        {
            this.poductService = poductService;
            _mapper = mapper;
        }



        //GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery] string? desc, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            List<Product> products = await poductService.GetProducts(desc, minPrice, maxPrice, categoryIds);

            return _mapper.Map<List<Product>, List<ProductDTO>>(products);



        }

        //GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(int id)

        {

            Product product = await poductService.GetProductbyId(id);
 
            return (product == null) ?
                 NoContent() : Ok(_mapper.Map<Product, ProductDTO>(product));

        }


    }
}
