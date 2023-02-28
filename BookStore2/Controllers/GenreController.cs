using AutoMapper;
using BookStore2.Application.BookOperations.Commands.CreateBooks;
using BookStore2.Application.BookOperations.Commands.DeleteBook;
using BookStore2.Application.BookOperations.Commands.UpdateBook;
using BookStore2.Application.BookOperations.Queries.GetBookDetail;
using BookStore2.Application.GenreOperations.Commands.CreaateGenre;
using BookStore2.Application.GenreOperations.Commands.DeleteGenre;
using BookStore2.Application.GenreOperations.Commands.UpdateGenre;
using BookStore2.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore2.Application.GenreOperations.Queries.GetGenres;
using BookStore2.DbOperations;
using BookStore2.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [Authorize]
    [ApiController] // This is a controller that will be used to handle API requests, controller actions will return an Http response
    [Route("[controller]s")] // Which controller will meet the requests coming to the WebApi is determined by these route attributes.
                             //Resource name: Genre
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // <summary>
        // Get all genres
        // </summary>
        // <returns></returns>
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Get a genre by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetGenreDetail([FromRoute] int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreID = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

        /// <summary>
        /// Add a genre
        /// </summary>
        /// <returns></returns>
        [HttpPost("FromBody")]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }

        /// <summary>
        /// Update a genre
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updatedGenre;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        /// <summary>
        /// Delete a genre from route
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}

