using AutoMapper;
using BookStore2.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore2.Application.AuthorOperations.Queries.GetAuthors;
using BookStore2.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore2.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore2.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore2.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{

    [ApiController] // This is a controller that will be used to handle API requests, controller actions will return an Http response
    [Route("[controller]s")] // Which controller will meet the requests coming to the WebApi is determined by these route attributes.                        //Resource name: Book
    public class AuthorController : ControllerBase
    {

        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // <summary>
        // Get all authors
        // </summary>
        // <returns></returns>
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Get a author by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetAuthorById([FromRoute] int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

        /// <summary>
        /// Add a author
        /// </summary>
        /// <returns></returns>
        [HttpPost("FromBody")]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newAuthor;
            
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            return Ok();
        }

         /// <summary>
        /// Update a author
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        /// <summary>
        /// Delete a author from route
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            return Ok();
        }
    }
}

