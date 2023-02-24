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
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 1
            };
            // act
            FluentActions.Invoking(() => command.Handle()).Invoke(); 
            // assert
            var book = _context.Books.SingleOrDefault(x => x.Title == command.Model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(command.Model.PageCount);
            book.PublishDate.Should().Be(command.Model.PublishDate);
            book.GenreId.Should().Be(command.Model.GenreId);
        } 


    }
}