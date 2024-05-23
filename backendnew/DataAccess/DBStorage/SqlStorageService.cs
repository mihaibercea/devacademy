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
        public void InsertBook(SqlBook sqlBook)
        {

            //connect to sql database and get all books
            using (var connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=BookStoreDB;Trusted_Connection=True;TrustServerCertificate=True"))
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
    }
}
