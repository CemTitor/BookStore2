using System.Linq;
using AutoMapper;
using BookStore2.BookOperations.CreateBooks;
using BookStore2.BookOperations.DeleteBook;
using BookStore2.BookOperations.GetBookDetail;
using BookStore2.BookOperations.UpdateBook;
using BookStore2.DbOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers;

[ApiController] // This is a controller that will be used to handle API requests, controller actions will return an Http response
[Route("[controller]s")] // Which controller will meet the requests coming to the WebApi is determined by these route attributes.
//Resource name: Book
public class BookController : ControllerBase
{

    private readonly BookStoreDbContext _context;

    private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    // <summary>
    // Get all books
    // </summary>
    // <returns></returns>
    [HttpGet]
    public IActionResult GetBooks()
    {
        GetbooksQuery query = new GetbooksQuery(_context, _mapper);
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
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
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
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        try
        {
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
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
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);

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
        try
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
