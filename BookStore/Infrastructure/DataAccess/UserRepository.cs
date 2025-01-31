﻿using System;
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
                    cmd.Parameters.Add("v_user_id", OracleDbType.Varchar2).Value = orderDTO.User_id;
                    cmd.Parameters.Add("v_book_id", OracleDbType.Int32).Value = orderDTO.Book_id;
                    cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = orderDTO.Quantity;
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
                                    Id = reader["id"] != DBNull.Value ? int.Parse(reader["id"].ToString()) : 0,
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

    }
}
