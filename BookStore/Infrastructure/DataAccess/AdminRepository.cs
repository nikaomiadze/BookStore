using Application.Interfaces;
using Domain.Entities;
using Oracle.ManagedDataAccess.Client;
using System;

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
            }
        }
    }
}
