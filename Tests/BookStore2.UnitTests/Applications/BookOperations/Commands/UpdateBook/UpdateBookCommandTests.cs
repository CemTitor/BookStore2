using AutoMapper;
using BookStore2.Application.BookOperations.Commands.UpdateBook;
using BookStore2.Entities;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

        }
        [Theory]
        [InlineData(50)]
        [InlineData(0)]
        [InlineData(-10)] // Fact means this is a test method
        public void WhenGivenBookIdIsNotFound_InvalidOperationException_ShouldBeReturn(int id)
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 999;
            command.Model = new UpdateBookModel()
            {
                Title = "Test Book",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = 1
            };
            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found!");
            // assert
        }


    }
}