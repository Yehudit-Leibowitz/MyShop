using Microsoft.AspNetCore.Mvc;
using service;
using System.Text.Json;
using Entity;
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
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService userService, IMapper mapper,ILogger<UsersController> logger)
        {
            this.userService = userService;
            _mapper = mapper;
            _logger = logger;
        }



        //GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> Get(int id)
        {
            // problem : doesnt shoes all prprties of orderitem 
            User foundUser = await userService.GetUserById(id);
            return foundUser == null? NoContent(): Ok(_mapper.Map<User, GetUserDTO>(foundUser)); 

        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<GetUserDTO>> Register([FromBody] RegisterUserDTO user)
        {
            User newUser = await userService.AddUser(_mapper.Map<RegisterUserDTO, User>(user));
            return newUser != null ? Ok(_mapper.Map<User, GetUserDTO>(newUser)) : BadRequest();

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
        public async Task<ActionResult<GetUserDTO>> LogIn([FromQuery] string userName, string password)
        {
            _logger.LogCritical($"Login attemped with Mail:{userName} and password :{password}");
            User userLogin = await userService.LogIn(userName, password);
            return (userLogin == null)?
                 Unauthorized():
           Ok(_mapper.Map<User, GetUserDTO>(userLogin));
           
            
           

        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetUserDTO>>Put(int id, [FromBody] RegisterUserDTO userToUpdate)
        {

            User updatedUser = await userService.UpdateUser(id, (_mapper.Map<RegisterUserDTO, User>(userToUpdate)));


            return updatedUser != null ? Ok(_mapper.Map<User, GetUserDTO>(updatedUser)) : BadRequest();


        }



    }
}
