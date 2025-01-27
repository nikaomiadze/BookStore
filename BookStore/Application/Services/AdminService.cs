using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
        }
        public void Add_book(Book book)
        {
            _adminRepository.Add_book(book);
        }
        public void Delete_book(int id) {
            _adminRepository.Delete_book(id);
        }
        public List<Order> Get_orders()
        {
             return _adminRepository.Get_orders();         
        }
        public void Complete_order(int id)
        {
            _adminRepository.Complete_order(id);
        }
        public List<Order> Get_user_order(int id)
        {
            return _adminRepository.Get_user_order(id);
        }

    }
}
