using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.FileStorage
{
    public class FileStorageService
    {
        public List<FileBook> GetAllBooks()
        {
            if (System.IO.File.Exists("books.json") == false)
            {
                System.IO.File.Create("books.json").Close();
                System.IO.File.WriteAllText("books.json", "[]");
            }

            var fileContent = System.IO.File.ReadAllText("books.json");
            var books = JsonSerializer.Deserialize<List<FileBook>>(fileContent);

            return books;
        }

        public FileBook SaveBook(FileBook fileBook)
        {
            if (System.IO.File.Exists("books.json") == false)
            {
                System.IO.File.Create("books.json").Close();
                System.IO.File.WriteAllText("books.json", "[]");
            }

            var fileContent = System.IO.File.ReadAllText("books.json");
            var books = JsonSerializer.Deserialize<List<FileBook>>(fileContent);

            books.Add(fileBook);

            var booksString = JsonSerializer.Serialize(books);
            System.IO.File.WriteAllText("books.json", booksString);

            return fileBook;
        }
    }
}
