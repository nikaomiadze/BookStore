using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
namespace BookStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userManagementService;

        public UserController(UserService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        [HttpPost("AddOrder")]
        public IActionResult Add_order([FromBody] Order order)
        {
            _userManagementService.Add_order(order);
            return Ok();
        }
    }
}
