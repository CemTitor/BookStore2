using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers;

[ApiController] // This is a controller that will be used to handle API requests, controller actions will return an Http response
[Route("[controller]s")] // Which controller will meet the requests coming to the WebApi is determined by these route attributes.
//Resource name: Book
public class BookController : ControllerBase
{
    private static List<Book> BookList = new List<Book>(){

        new Book{
            Id =1,
            Title="The Fountainhead",
            GenreId=1,
            PageCount=500,
            PublishDate=DateTime.Now.AddYears(-10),
        },
        new Book{
            Id =2,
            Title="Herland",
            GenreId=2,
            PageCount=250,
            PublishDate=DateTime.Now.AddYears(-10),
        },
        new Book{
            Id =3,
            Title="Dune",
            GenreId=2,
            PageCount=600,
            PublishDate=DateTime.Now.AddYears(-10),
        }

    };

    private readonly ILogger<BookController> _logger;

    // The constructor is used to inject dependencies( like ILogger<BookController>) into the controller
    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }

    // <summary>
    // Get all books
    // </summary>
    // <returns></returns>
    [HttpGet]
    public IActionResult GetBooks()
    {
        var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        if (bookList == null)
        {
            return NotFound();
        }
        return Ok(bookList);
    }

    /// <summary>
    /// Get a book by id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetBookById([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    /// <summary>
    /// Add a book
    /// </summary>
    /// <returns></returns>
    [HttpPost("FromBody")]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
        if (book != null)
        {
            return BadRequest("A book with the same title already exists.");
        }

        BookList.Add(newBook);
        return Ok();
    }

    /// <summary>
    /// Update a book
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book is null)
        {
            return BadRequest();
        }

        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        return Ok();
    }

    /// <summary>
    /// Delete a book from route
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        BookList.Remove(book);
        return Ok();
    }

    /// <summary>
    /// Delete a book from query
    /// </summary>
    /// <returns></returns>
    [HttpDelete("FromQuery")]
    public IActionResult DeleteBookFromQuery([FromQuery] int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        BookList.Remove(book);
        return Ok();
    }

    /// <summary>
    /// Get list of books by name
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public IActionResult GetBooksByName([FromQuery] string bookName)
    {
        var bookList = BookList.Where(x => x.Title.ToUpper().Contains(bookName.ToUpper())).OrderBy(x => x.Title).ToList<Book>();
        if (bookList == null)
        {
            return NotFound();
        }
        return Ok(bookList);
    }

    /// <summary>
    /// Sort books by page count in descending order
    /// </summary>
    /// <returns></returns>
    [HttpGet("sorting_desc")]
    public IActionResult GetBooksDesc()
    {
        var bookList = BookList.ToList<Book>();
        if (bookList == null)
        {
            return NotFound();
        }

        bookList = bookList.OrderByDescending(b => b.PageCount).ToList();
        return Ok(bookList);
    }

    /// <summary>
    /// Sort books by page count in ascending order
    /// </summary>
    /// <returns></returns>
    [HttpGet("sorting_asc")]
    public IActionResult GetBooksAsc()
    {
        var bookList = BookList.ToList<Book>();
        if (bookList == null)
        {
            return NotFound();
        }

        bookList = bookList.OrderBy(b => b.PageCount).ToList();
        return Ok(bookList);
    }


}
