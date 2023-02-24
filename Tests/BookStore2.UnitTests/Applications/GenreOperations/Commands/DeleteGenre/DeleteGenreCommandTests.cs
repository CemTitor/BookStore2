using AutoMapper;
using BookStore2.Application.GenreOperations.Commands.DeleteGenre;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;

namespace Application.BookOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact] // Fact means this is a test method
        public void WhenThereIsNoGenreWithGivenId_InvalidOperationException_ShouldBeReturn(int id)
        {
            // arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not found!");
            // assert
        }
    }
}