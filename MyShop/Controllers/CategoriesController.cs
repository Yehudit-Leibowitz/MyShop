using Microsoft.AspNetCore.Mvc;
using Entity;
using service;
using DTO;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        ICategoryService categoryService;
        IMapper _mapper;
        IMemoryCache _memoryCache;
        public CategoriesController(ICategoryService categoryService, IMapper mapper, IMemoryCache memoryCache)
        {
            this.categoryService = categoryService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }


      

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            if (!_memoryCache.TryGetValue("categories", out List<Category> categories))
            {
                categories = await categoryService.GetAllCategory();
                _memoryCache.Set("categories", categories, TimeSpan.FromMinutes(1));
            }
            return Ok(_mapper.Map<List<Category>, List<CategoryDTO>>(categories));
        }


    }
}


