using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.DTOs;


namespace Domain.Interfaces
{
    public interface IUserRepository
    {
         void  Add_Order(Order order);
        User? authentification(LoginDTO loginData);
        void Add_User(User user);



    }
}
