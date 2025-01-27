﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminRepository
    {
        void Add_book(Book book);
        void Delete_book(int id);
        List<Order> Get_orders();
        void Complete_order(int id);
        List<Order> Get_user_order(int id);

    }
}
