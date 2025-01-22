using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface;
using Oracle.ManagedDataAccess.Client;

namespace Infrastructure.DataAccess
{
    public class UserRepository(string connectionString) : IUserRepository
    {
        private readonly string _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

        public void Add_Order(Order order)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_BOOKSTORE_USER.add_order", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_user_id", OracleDbType.Int32).Value = order.User_id;
                    cmd.Parameters.Add("v_book_id", OracleDbType.Int32).Value = order.Book_id;
                    cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = order.Quantity;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
