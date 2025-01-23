using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    using Domain.DTOs;
    using Domain.Entities;

    public interface IUserService
    {
        void Add_order(Order order);
        User? authentification(LoginDTO loginData);
        void Add_User(User user);



    }
}
