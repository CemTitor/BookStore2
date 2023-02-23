using AutoMapper;
using BookStore2.Application.BookOperations.Commands.CreateBooks;
using BookStore2.Entities;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact] // Fact means this is a test method
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var book = new Book()
            {
                Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand bookModel = new CreateBookCommand(_context, _mapper);
            bookModel.Model = new CreateBookModel()
            {
                Title = book.Title,
            };
            // act and assert
            FluentActions.Invoking(() => bookModel.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book already exists!");

        }
    }
}