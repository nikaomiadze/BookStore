using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.DataAccess
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void Add_book(Book book)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKS.Add_book", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_book_name", OracleDbType.Varchar2).Value = book.Book_name;
                    cmd.Parameters.Add("v_author", OracleDbType.Varchar2).Value = book.Author;
                    cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = book.Quantity;
                    cmd.Parameters.Add("v_price", OracleDbType.Int32).Value = book.Price;

                    cmd.ExecuteNonQuery();
                }
                conn.Close();

            }
        }

        public void Delete_book(int id)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKS.delete_book", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

            }
        }
        public List<Order> Get_orders() 
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open ();
                List<Order> orders = new List<Order>();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_ORDERS.get_orders", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Username = reader["user_name"].ToString(),
                            User_id = int.Parse(reader["user_id"].ToString()),
                            Book_id = int.Parse(reader["book_id"].ToString()),
                            Quantity = int.Parse(reader["quantity"].ToString()),
                            Order_price = int.Parse(reader["order_price"].ToString())
                        };

                        orders.Add(order);
                    }
                    conn.Close ();
                    return orders;
                }
            }
        }
        public void Complete_order(int id)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_ORDERS.complete_order", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_order_id", OracleDbType.Int32).Value = id;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

            }

        }
        public List<UserOrderDTO> Get_user_order(int id)
        {
            List<UserOrderDTO> orders = new List<UserOrderDTO>();

            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKSTORE_ORDERS.get_user_order", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Value = id;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                UserOrderDTO order = new UserOrderDTO
                                {
                                    Id = reader["id"] != DBNull.Value ? Convert.ToInt32(reader["id"]) : 0,
                                    UserName = reader["user_name"]?.ToString(),
                                    Quantity = reader["quantity"] != DBNull.Value ? Convert.ToInt32(reader["quantity"]) : 0,
                                    BookName = reader["book_name"]?.ToString(),
                                    Author = reader["author"]?.ToString(),
                                    Order_Price = reader["order_price"] != DBNull.Value ? Convert.ToInt32(reader["order_price"]) : 0,
                                };

                                orders.Add(order);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error mapping data: {ex.Message}");
                            }
                        }
                    }
                    conn.Close();
                }
            }

            return orders;
        }
        public List<Book> Get_books()
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                List<Book> books = new List<Book>();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKS.get_book", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Book_name = reader["book_name"].ToString(),
                            Author = reader["author"].ToString(),
                            Quantity = int.Parse(reader["quantity"].ToString()),
                            Price = int.Parse(reader["price"].ToString())
                        };

                        books.Add(book);
                    }
                    conn.Close();
                    return books;
                }
            }
        }
        public List<Book> Get_books_byID(int id)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                List<Book> books = new List<Book>();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKS.get_book_by_id", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Book_name = reader["book_name"].ToString(),
                            Author = reader["author"].ToString(),
                            Quantity = int.Parse(reader["quantity"].ToString()),
                            Price = int.Parse(reader["price"].ToString())
                        };

                        books.Add(book);
                    }
                    conn.Close();
                    return books;
                }
            }
        }
        public void Update_Book(Book book)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("olerning.PKG_NO_BOOKS.update_book", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = book.Id;
                    cmd.Parameters.Add("p_book_name", OracleDbType.Varchar2).Value =
                        book.Book_name=="string" ? DBNull.Value : book.Book_name;
                    cmd.Parameters.Add("p_author", OracleDbType.Varchar2).Value =
                        book.Author=="string" ? DBNull.Value : book.Author;
                    cmd.Parameters.Add("p_quantity", OracleDbType.Int32).Value =
                        book.Quantity > 0 ? book.Quantity : DBNull.Value;
                    cmd.Parameters.Add("p_price", OracleDbType.Int32).Value =
                        book.Price > 0 ? book.Price : DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
                conn.Clone();
            }
        }

    }
}
