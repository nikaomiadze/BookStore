using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;


namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void Add_order(OrderDTO orderDTO)
        {
            _userRepository.Add_Order(orderDTO);
        }
        public User? authentification(LoginDTO loginData)
        {
           return _userRepository.authentification(loginData);
        }
        public void Add_User(User user)
        {
            _userRepository.Add_User(user);
        }
        public void Add_in_cart(CartDTO cart) 
        {
            _userRepository.Add_in_cart(cart);
        }
        public List<Cart> Get_user_cart(int id)
        {
            return _userRepository.Get_user_cart(id);
        }
        public List<UserDTO> Get_user_by_id(int id)
        {
            return _userRepository.Get_user_by_id(id);
        }
        public void Delete_cart_item(int id) 
        {
            _userRepository.Delete_cart_item(id);
        }



    }
}
