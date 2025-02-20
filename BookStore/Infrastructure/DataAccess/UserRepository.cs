using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using Application.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Infrastructure.DataAccess
{
    public class UserRepository: IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public void Add_Order(OrderDTO orderDTO)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_ORDERS.add_order", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_user_id", OracleDbType.Int32).Value = orderDTO.User_id;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public User? authentification(LoginDTO loginData)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open ();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_USERS.auth_user", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = loginData.Email;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = loginData.Password;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    User? user = null;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                user = new User
                                {
                                    Id = reader["userid"] != DBNull.Value ? int.Parse(reader["userid"].ToString()) : 0,
                                };
                            }
                        }

                        conn.Close();
                        return user;

                    }
                }


            }

        }
        public void Add_User(User user)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open ();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_USERS.user_register", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_user_name", OracleDbType.Varchar2).Value = user.User_name;
                    cmd.Parameters.Add("v_password", OracleDbType.Varchar2).Value = user.Password;
                    cmd.Parameters.Add("v_email", OracleDbType.Varchar2).Value = user.Email;
                    cmd.ExecuteNonQuery();

                }
                conn.Close();

            }

        }
        public void Add_in_cart(CartDTO cart) 
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_USERS.add_to_cart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_user_id", OracleDbType.Int32).Value = cart.User_id;
                    cmd.Parameters.Add("v_book_id", OracleDbType.Int32).Value = cart.Book_id;
                    cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = cart.Quantity;
                    cmd.ExecuteNonQuery();

                }
                conn.Close();

            }
        }
        public List<Cart> Get_user_cart(int id)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                List<Cart> cart = new List<Cart>();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_USERS.get_user_cart", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cart book = new Cart
                        {
                            Id = int.Parse(reader["CartItemID"].ToString()),
                            Cart_id = int.Parse(reader["CartID"].ToString()),
                            User_id = int.Parse(reader["UserID"].ToString()),
                            Book_name = reader["book_name"].ToString(),
                            Author = reader["Author"].ToString(),
                            Quantity = int.Parse(reader["Quantity"].ToString()),
                            Order_price = int.Parse(reader["TotalPrice"].ToString()),
                        };

                        cart.Add(book);
                    }
                    conn.Close();
                    return cart;
                }
            }
        }
        public List<UserDTO> Get_user_by_id(int id)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                List<UserDTO> user = new List<UserDTO>();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_USERS.get_user_by_id", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_id", OracleDbType.Int32).Value = id;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UserDTO user1 = new UserDTO
                        {
                            Id = int.Parse(reader["userid"].ToString()),
                            User_Name = reader["user_name"].ToString(),
                            Email = reader["email"].ToString(),
                            User_role = reader["role"].ToString(),

                        };

                        user.Add(user1);
                    }
                    conn.Close();
                    return user;
                }
            }
        }
        public void Delete_cart_item(int id)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_USERS.delete_cart_item", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_id", OracleDbType.Int32).Value = id;
                   
                    cmd.ExecuteNonQuery();

                }
                conn.Close();

            }
        }

    }
}
