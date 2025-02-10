using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
     public class CartDTO
    {
        public int Id { get; set; }
        public int? User_id { get; set; }
        public int? Book_id { get; set; }
        public int? Quantity { get; set; }
    }
}
