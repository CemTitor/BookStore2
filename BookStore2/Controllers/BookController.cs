using System.Linq;
using BookStore2.DbOperations;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers;

[ApiController] // This is a controller that will be used to handle API requests, controller actions will return an Http response
[Route("[controller]s")] // Which controller will meet the requests coming to the WebApi is determined by these route attributes.
//Resource name: Book
public class BookController : ControllerBase
{

    private readonly BookStoreDbContext _context;

    public BookController(BookStoreDbContext context)
    {
        _context = context;
    }

    // private static List<Book> BookList = new List<Book>(){
    //     new Book{
    //         Id =1,
    //         Title="The Fountainhead",
    //         GenreId=1,
    //         PageCount=500,
    //         PublishDate=DateTime.Now.AddYears(-10),
    //     },
    //     new Book{
    //         Id =2,
    //         Title="Herland",
    //         GenreId=2,
    //         PageCount=250,
    //         PublishDate=DateTime.Now.AddYears(-10),
    //     },
    //     new Book{
    //         Id =3,
    //         Title="Dune",
    //         GenreId=2,
    //         PageCount=600,
    //         PublishDate=DateTime.Now.AddYears(-10),
    //     }

    // };

    // <summary>
    // Get all books
    // </summary>
    // <returns></returns>
    [HttpGet]
    public IActionResult GetBooks()
    {
        GetbooksQuery query = new GetbooksQuery(_context);
        var result = query.Handle();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// Get a book by id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetBookById([FromRoute] int id)
    {
        BookDetailViewModel result = null;
        try
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context);
            query.BookId = id;
            result = query.Handle();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(result);
    }

    /// <summary>
    /// Add a book
    /// </summary>
    /// <returns></returns>
    [HttpPost("FromBody")]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(_context);

        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            command.NewBook = newBook;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();

    }

    /// <summary>
    /// Update a book
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }
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
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);

        _context.SaveChanges();
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
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);

        _context.SaveChanges();
        return Ok();
    }

    /// <summary>
    /// Get list of books by name
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public IActionResult GetBooksByName([FromQuery] string bookName)
    {
        var bookList = _context.Books.Where(x => x.Title.ToUpper().Contains(bookName.ToUpper())).OrderBy(x => x.Title).ToList<Book>();
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
        var bookList = _context.Books.ToList<Book>();
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
        var bookList = _context.Books.ToList<Book>();
        if (bookList == null)
        {
            return NotFound();
        }

        bookList = bookList.OrderBy(b => b.PageCount).ToList();
        return Ok(bookList);
    }


}
