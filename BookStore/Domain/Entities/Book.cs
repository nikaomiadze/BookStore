﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book
    {
        public int? Id { get; set; }
        public string? Book_name { get; set; }
        public string? Author { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
    }
}
