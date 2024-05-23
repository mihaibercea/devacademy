using BusinessLogic.Books;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private BooksService booksService = new BooksService();

        [HttpPost]
        [Consumes("application/json")]
        public BookDTO Post([FromBody] BookDTO book)
        {
            var newBook = MapBookDTOToBook(book);

            var insertedBook = booksService.InsertBook(newBook);

            var insertedBookDTO = new BookDTO
            {
                ISBN13 = insertedBook.ISBN13,
                ISBN10 = insertedBook.ISBN10,
                Title = insertedBook.Title,
                Author = insertedBook.Author,
                PublishedDate = insertedBook.PublishedDate,
                NumberOfPages = insertedBook.NumberOfPages,
                Publisher = insertedBook.Publisher,
                ReviewScore = insertedBook.ReviewScore
            };

            return insertedBookDTO;
        }


        [HttpGet]
        public List<BookDTO> Get()
        {
            var books = booksService.GetBooks();

            var booksDTO = new List<BookDTO>();
            foreach (var book in books)
            {
                var bookDTO = new BookDTO
                {
                    BookId = book.BookId,
                    ISBN13 = book.ISBN13,
                    ISBN10 = book.ISBN10,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedDate = book.PublishedDate,
                    NumberOfPages = book.NumberOfPages,
                    Publisher = book.Publisher,
                    ReviewScore = book.ReviewScore
                };

                booksDTO.Add(bookDTO);
            }

            return booksDTO;
        }

        [HttpGet]
        [Route("{bookId}")]
        public BookDTO Get([FromRoute] string bookId)
        {
            var book = booksService.GetBookById(bookId);

            BookDTO bookDTO = MapBookToBookDTO(book);

            return bookDTO;
        }

        [HttpGet]
        [Route("/isbn/{isbn10}")]
        public BookDTO GetBookByIsbn([FromRoute] string isbn10)
        {
            var book = booksService.GetBookByIsbn(isbn10);

            BookDTO bookDTO = MapBookToBookDTO(book);

            return bookDTO;
        }

        [HttpPut]
        [Consumes("application/json")]
        public BookDTO Put([FromBody] BookDTO book)
        {
            var newBook = MapBookDTOToBook(book);

            var updatedBook = booksService.UpdateBook(newBook);

            var bookDTO = MapBookToBookDTO(updatedBook);

            return bookDTO;
        }


        private static Book MapBookDTOToBook(BookDTO book)
        {
            return new BusinessLogic.Books.Book
            {
                Title = book.Title,
                Author = book.Author,
                ISBN10 = book.ISBN10,
                ISBN13 = book.ISBN13,
                PublishedDate = book.PublishedDate,
                NumberOfPages = book.NumberOfPages,
                Publisher = book.Publisher,
                ReviewScore = book.ReviewScore
            };
        }



        private static BookDTO MapBookToBookDTO(Book book)
        {
            return new BookDTO
            {
                BookId = book.BookId,
                ISBN13 = book.ISBN13,
                ISBN10 = book.ISBN10,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                NumberOfPages = book.NumberOfPages,
                Publisher = book.Publisher,
                ReviewScore = book.ReviewScore
            };
        }



        [HttpPost]
        [Consumes("application/json")]
        [Route("review")]
        public void PostReview([FromBody] BookReviewDTO bookReviewDTO)
        {
            var bookReview = new BookReview
            {
                ISBN10 = bookReviewDTO.ISBN10,
                ReviewScore = float.Parse(bookReviewDTO.ReviewScore),
                ReviewText = bookReviewDTO.ReviewText
            };
            booksService.AddBookReview(bookReview);
        }
    }
}
