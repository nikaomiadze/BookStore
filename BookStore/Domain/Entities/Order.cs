using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int? Order_Id { get; set; }
        public string? Username { get; set; }
        public int? Order_price { get; set; }
        public string? Order_date { get; set; }
        public string? Order_Status { get; set; }
    }
}
