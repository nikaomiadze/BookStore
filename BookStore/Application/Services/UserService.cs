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

        public void Add_order(Order order)
        {
            _userRepository.Add_Order(order);
        }
        public User? authentification(LoginDTO loginData)
        {
           return _userRepository.authentification(loginData);
        }
        public void Add_User(User user)
        {
            _userRepository.Add_User(user);
        }
        
    }
}
