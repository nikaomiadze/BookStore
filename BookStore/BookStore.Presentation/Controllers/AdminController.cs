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
        [HttpPost]
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
        [HttpDelete]
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

    }
}
