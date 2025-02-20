using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? User_Name { get; set; }
        public string? Email { get; set; }
        public string? User_role { get; set; }
    }
}
