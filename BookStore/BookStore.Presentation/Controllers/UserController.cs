using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
using Application.Interfaces;
using Domain.DTOs;
namespace BookStore.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IjwtManager _jwtManager;

        public UserController(IUserService userService,IjwtManager ijwtManager)
        {
            _userService = userService;
            _jwtManager = ijwtManager;

        }

        [HttpPost]
        public IActionResult Add_order([FromBody] Order order)
        {
            _userService.Add_order(order);
            return Ok();
        }
        [HttpPost("/login_user")]
        public IActionResult Authentification(LoginDTO loginData)
        {
            Token? token = null;
            User? user = null;

            try
            {
                user = _userService.authentification(loginData);
                if (user == null) return Unauthorized("username or password is inccorect");

                token = _jwtManager.GetToken(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "System error,try again");

            }
            return Ok(token);


        }
        [HttpPost]
        public IActionResult Add_User([FromBody] User user)
        {
           _userService.Add_User(user);
            return Ok();
        }
    }
}
