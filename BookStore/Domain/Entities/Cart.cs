using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int? User_id { get; set; }
        public string? Book_name { get; set; }
        public string? Author { get; set; }
        public int? Quantity { get; set; }
        public int? Cart_id { get; set; }
        public int? Order_price { get; set; }


    }
}
