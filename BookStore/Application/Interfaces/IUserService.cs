using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    using Application.DTOs;
    using Domain.Entities;

    public interface IUserService
    {
        void Add_order(OrderDTO orderDTO);
        User? authentification(LoginDTO loginData);
        void Add_User(User user);
        void Add_in_cart(CartDTO cart);
        List<Cart> Get_user_cart(int id);
        List<UserDTO> Get_user_by_id(int id);

    }
}
