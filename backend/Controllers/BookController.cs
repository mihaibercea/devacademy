using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DotNetWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {

        public BookController()
        {

        }

        [HttpGet]
        public List<Book> Get()
        {
            if (System.IO.File.Exists("books.json") == false)
            {
                return new List<Book>();
            }
            string jsonString = System.IO.File.ReadAllText("books.json");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString)!;
            return books;
        }

        [HttpGet]
        //add /i to the route and get i as an integer
        [Route("{isbn10}")]
        public Book Get([FromRoute] string isbn10)
        {
            string jsonString = System.IO.File.ReadAllText("books.json");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString)!;

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ISBN10 == isbn10)
                {
                    return books[i];
                }
            }

            return null;

        }

        [HttpPost]
        //add application/json to the route
        [Consumes("application/json")]
        //create post method to update user data, get userId and userData from the body
        public Book Post([FromBody] Book book)
        {
            if (System.IO.File.Exists("books.json") == false)
            {
                System.IO.File.Create("books.json").Close();
                System.IO.File.WriteAllText("books.json", "[]");
            }
            string jsonString = System.IO.File.ReadAllText("books.json");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString)!;

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ISBN10 == book.ISBN10
                    || books[i].ISBN13 == book.ISBN13)
                    return null;
            }
            
            books.Add(book);

            var booksString = JsonSerializer.Serialize(books);
            System.IO.File.WriteAllText("books.json", booksString);

            return book;
        }

        [HttpPut]
        public string UpdateBook(Book book)
        {
            string jsonString = System.IO.File.ReadAllText("books.json");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString)!;

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ISBN10 == book.ISBN10)
                {
                    books[i].ISBN13 = book.ISBN13;
                    books[i].Title = book.Title;
                    books[i].Author = book.Author;
                    books[i].PublishedDate = book.PublishedDate;
                    books[i].NumberOfPages = book.NumberOfPages;
                    books[i].Publisher = book.Publisher;
                    books[i].ReviewScore = book.ReviewScore;

                    var booksString = JsonSerializer.Serialize(books);
                    System.IO.File.WriteAllText("books.json", booksString);

                    return "Updated";
                }
            }

            return "Not found";
        }

        [HttpDelete]
        //route http://localhost:5077/api/book/1234567890
        [Route("{isbn10}")]
        public string DeleteBook([FromRoute] string isbn10)
        {
            string jsonString = System.IO.File.ReadAllText("books.json");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString)!;

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ISBN10 == isbn10)
                {
                    books.RemoveAt(i);
                    break;
                }
            }

            var booksString = JsonSerializer.Serialize(books);
            System.IO.File.WriteAllText("books.json", booksString);

            return "Deleted";
        }

    }
}
