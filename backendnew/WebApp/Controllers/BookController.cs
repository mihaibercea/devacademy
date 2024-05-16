using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        [HttpPost]
        [Consumes("application/json")]
        public Book Post([FromBody] Book book)
        {
            if (System.IO.File.Exists("books.json") == false)
            {
                System.IO.File.Create("books.json").Close();
                System.IO.File.WriteAllText("books.json", "[]");
            }

            var fileContent = System.IO.File.ReadAllText("books.json");
            var books = JsonSerializer.Deserialize<List<Book>>(fileContent);

            books.Add(book);

            var booksString = JsonSerializer.Serialize(books);
            System.IO.File.WriteAllText("books.json", booksString);

            return book;
        }
    }
}
