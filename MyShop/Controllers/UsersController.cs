using Microsoft.AspNetCore.Mvc;
using service;
using System.Text.Json;
using Entity;
using service;
using AutoMapper;
using DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService userService;
        IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            _mapper = mapper;
        }



        //GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> Get(int id)
        {

            User foundUser = await userService.GetUserById(id);
            if (foundUser == null)
                return NoContent();
            else

                return Ok(_mapper.Map<User, GetUserDTO>(foundUser));

        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO user)
        {
            User newUser = await userService.AddUser(_mapper.Map<RegisterUserDTO, User>(user));
            return newUser != null ? Ok((_mapper.Map<User, GetUserDTO>(newUser))) : Unauthorized();
        }

        [HttpPost("password")]
        public async Task<IActionResult> CheckPassword([FromBody] string password)
        {

            int Score = userService.CheckPassword(password);

            return (Score < 3) ?
                   BadRequest(Score) :
              Ok(Score);
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> LogIn([FromQuery] string userName, string password)
        {
            User userLogin = await userService.LogIn(userName, password);
            if (userLogin == null)
                return NoContent();
            else
                return Ok(userLogin);


        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] User userToUpdate)
        {
            await userService.UpdateUser(id, userToUpdate);
        }



    }
}
