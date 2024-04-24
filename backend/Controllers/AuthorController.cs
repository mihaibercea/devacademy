using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DotNetWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        [HttpPost]
        //add application/json to the route
        [Consumes("application/json")]
        //create post method to update user data, get userId and userData from the body
        public Author Post([FromBody] Author author)
        {
            if (System.IO.File.Exists("authors.json") == false)
            {
                System.IO.File.Create("authors.json").Close();
                System.IO.File.WriteAllText("authors.json", "[]");
            }
            string jsonString = System.IO.File.ReadAllText("authors.json");
            List<Author> authors = JsonSerializer.Deserialize<List<Author>>(jsonString)!;

            authors.Add(author);

            var authorsString = JsonSerializer.Serialize(authors);
            System.IO.File.WriteAllText("authors.json", authorsString);

            return author;
        }


        [HttpGet]
        public List<Author> Get()
        {
            string jsonString = System.IO.File.ReadAllText("authors.json");
            List<Author> authors = JsonSerializer.Deserialize<List<Author>>(jsonString)!;
            return authors;
        }
    }
}
