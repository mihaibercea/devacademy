using DataAccess.DBStorage;
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
        private SqlStorageService sqlStorageService = new SqlStorageService();

        public Book InsertBook(Book book)
        {
            //var books = sqlStorageService.GetAllBooks();

            //for (var i = 0; i < books.Count; i++)
            //{
            //    if (books[i].ISBN10 == book.ISBN10 || books[i].ISBN13 == book.ISBN13)
            //    {
            //        return null;
            //    }
            //}

            //map book to filebook
            var sqlBook = new SqlBook
            {
                Author = book.Author,
                ISBN10 = book.ISBN10,
                ISBN13 = book.ISBN13,
                NumberOfPages = book.NumberOfPages,
                PublishedDate = book.PublishedDate,
                Publisher = book.Publisher,
                ReviewScore = (float)book.ReviewScore,
                Title = book.Title
            };

            sqlStorageService.InsertBook(sqlBook);


            return book;
        }

        public List<Book> GetBooks()
        {
            var sqlBooks = sqlStorageService.GetAllBooks();

            //map filebook to book
            var books = new List<Book>();
            foreach (var sqlBook in sqlBooks)
            {
                var book = MapSqlBookToBook(sqlBook);
                books.Add(book);
            }

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

        public Book GetBookById(string id)
        {
            var book = sqlStorageService.GetBookById(id);
            return MapSqlBookToBook(book);
            
        }

        public Book GetBookByIsbn(string isbn10)
        {
            var fileContent = System.IO.File.ReadAllText("books.json");
            var books = JsonSerializer.Deserialize<List<Book>>(fileContent);

            var book = books.FirstOrDefault(b => b.ISBN10 == isbn10);
            return book;
        }

        private Book MapFileBookToBook(FileBook fileBook)
        {
            var book = new Book
            {
                Author = fileBook.Author,
                ISBN10 = fileBook.ISBN10,
                ISBN13 = fileBook.ISBN13,
                NumberOfPages = fileBook.NumberOfPages,
                PublishedDate = fileBook.PublishedDate,
                Publisher = fileBook.Publisher,
                ReviewScore = fileBook.ReviewScore,
                Title = fileBook.Title
            };

            return book;
        }

        private Book MapSqlBookToBook(SqlBook sqlBook)
        {
            var book = new Book
            {
                BookId = sqlBook.BookId,
                Author = sqlBook.Author,
                ISBN10 = sqlBook.ISBN10,
                ISBN13 = sqlBook.ISBN13,
                NumberOfPages = sqlBook.NumberOfPages,
                PublishedDate = sqlBook.PublishedDate,
                Publisher = sqlBook.Publisher,
                ReviewScore = sqlBook.ReviewScore,
                Title = sqlBook.Title
            };

            return book;
        }

        public void AddBookReview(BookReview bookReview)
        {
            Console.WriteLine();
        }
    }
}
