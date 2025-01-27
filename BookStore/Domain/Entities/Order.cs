﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public int? User_id { get; set; }
        public int? Book_id { get; set; }
        public string? Book_name { get; set; }
        public int? Quantity { get; set; }
        public int? Order_price { get; set; }
    }
}
