using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        void Add_Order(OrderDTO orderDTO);
        User? authentification(LoginDTO loginData);
        void Add_User(User user);

    }
}
