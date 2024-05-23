using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

namespace DataAccess.DBStorage
{
    public class SqlStorageService
    {
        private readonly string connectionString = "Server=localhost\\SQLEXPRESS;Database=BookStoreDB;Trusted_Connection=True;TrustServerCertificate=True";
        public void InsertBook(SqlBook sqlBook)
        {

            //connect to sql database and get all books
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO Books " +
                    $"(Title, Author, ISBN10, ISBN13, PublishedDate, NumberOfPages, Publisher, ReviewScore)" +
                    $"\r\nVALUES ('{sqlBook.Title}', '{sqlBook.Author}', '{sqlBook.ISBN10}', '{sqlBook.ISBN13}', '{sqlBook.PublishedDate}', '{sqlBook.NumberOfPages}', '{sqlBook.Publisher}', '{sqlBook.ReviewScore}');";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var value = reader.GetString(0);
                    }
                }
            }
        }

        public List<SqlBook> GetAllBooks()
        {
            var books = new List<SqlBook>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM Books";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var newBook = new SqlBook();
                        newBook.BookId = reader.GetInt32(0);
                        newBook.Title = reader.GetString(1);
                        newBook.Author = reader.GetString(2);
                        newBook.ISBN10 = reader.GetString(3);
                        newBook.ISBN13 = reader.GetString(4);
                        newBook.PublishedDate = reader.GetDateTime(5).ToShortDateString();
                        newBook.NumberOfPages = reader.GetInt32(6);
                        newBook.Publisher = reader.GetString(7);
                        newBook.ReviewScore = (float)reader.GetDouble(8);
                        books.Add(newBook);
                    }
                }

            }

            return books;
        }
    }
}
