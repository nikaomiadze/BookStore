using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserOrderDTO
    {
        public int? Id {  get; set; }
        public string? UserName { get; set; }
        public string? BookName { get; set; }
        public int? Quantity { get; set; }
        public string? Author { get; set; }
        public int? Order_Price { get; set; }
        public string? Order_Date { get; set; }
        public string? Order_Status { get; set; }

    }
}
