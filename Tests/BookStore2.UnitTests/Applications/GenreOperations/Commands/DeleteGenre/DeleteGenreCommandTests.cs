using AutoMapper;
using BookStore2.Application.GenreOperations.Commands.DeleteGenre;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Theory] // Fact means this is a test method
        [InlineData(1)]
        [InlineData(2)]
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