using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("/add_book")]
        public IActionResult Add_Book([FromBody] Book book)
        {
            try
            {
                _adminService.Add_book(book);
                return Ok("book added successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, ex.Message);

            }
        }
        [HttpDelete("/delete_book")]
        public IActionResult Delete_book([FromBody] int id)
        {
            try
            {
                _adminService.Delete_book(id);
                return Ok("book deleted successfully.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, ex.Message);

            }
        }
        [HttpGet("/get_orders")]
        public List<Order> Get_Orders()
        {
            List<Order> list = new List<Order>();
            try
            {
                list = _adminService.Get_orders();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }
        [HttpPost("/complete_order")]
        public IActionResult Complete_order(int id)
        {

            _adminService.Complete_order(id);
            return Ok("order completed successfully.");

        }
        [HttpGet("/get_user_orders")]
        public List<Order> Get_user_orders(int id)
        {
            List<Order> list = new List<Order>();
            try
            {
                list = _adminService.Get_user_order(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }

    }
}
