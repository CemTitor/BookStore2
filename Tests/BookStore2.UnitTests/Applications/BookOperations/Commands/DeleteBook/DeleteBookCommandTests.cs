using AutoMapper;
using BookStore2.Application.BookOperations.Commands.DeleteBook;
using BookStore2.Entities;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact] // Fact means this is a test method
        public void WhenThereIsNoBookWithGivenId_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 999;
            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found!");
            // assert
        }
    }
}