using DataAccess.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Books
{
    public class BooksService
    {
        private FileStorageService fileStorageService = new FileStorageService();

        public Book InsertBook(Book book)
        {
            var books = fileStorageService.GetAllBooks();

            for (var i = 0; i < books.Count; i++)
            {
                if (books[i].ISBN10 == book.ISBN10 || books[i].ISBN13 == book.ISBN13)
                {
                    return null;
                }
            }

            //map book to filebook
            var fileBook = new FileBook
            {
                Author = book.Author,
                ISBN10 = book.ISBN10,
                ISBN13 = book.ISBN13,
                NumberOfPages = book.NumberOfPages,
                PublishedDate = book.PublishedDate,
                Publisher = book.Publisher,
                ReviewScore = book.ReviewScore,
                Title = book.Title
            };

            fileStorageService.SaveBook(fileBook);


            return book;
        }

        public List<Book> GetBooks()
        {
            var fileContent = System.IO.File.ReadAllText("books.json");
            var books = JsonSerializer.Deserialize<List<Book>>(fileContent);

            return books;
        }

        public Book UpdateBook(Book udatedBook)
        {

            var fileContent = System.IO.File.ReadAllText("books.json");
            var books = JsonSerializer.Deserialize<List<Book>>(fileContent);

            var bookToUpdate = books.FirstOrDefault(b => b.ISBN10 == udatedBook.ISBN10 || b.ISBN13 == udatedBook.ISBN13);

            if (bookToUpdate == null)
            {
                return null;
            }

            bookToUpdate.Author = udatedBook.Author;
            bookToUpdate.NumberOfPages = udatedBook.NumberOfPages;
            bookToUpdate.PublishedDate = udatedBook.PublishedDate;
            bookToUpdate.Publisher = udatedBook.Publisher;
            bookToUpdate.ReviewScore = udatedBook.ReviewScore;
            bookToUpdate.Title = udatedBook.Title;


            var booksString = JsonSerializer.Serialize(books);
            System.IO.File.WriteAllText("books.json", booksString);


            return bookToUpdate;

        }

        public Book GetBookByIsbn(string isbn10)
        {
            var fileContent = System.IO.File.ReadAllText("books.json");
            var books = JsonSerializer.Deserialize<List<Book>>(fileContent);

            var book = books.FirstOrDefault(b => b.ISBN10 == isbn10);
            return book;
        }
    }
}
