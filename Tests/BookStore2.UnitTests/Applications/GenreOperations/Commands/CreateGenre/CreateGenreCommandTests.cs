using AutoMapper;
using BookStore2.Entities;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;
using BookStore2.Application.GenreOperations.Commands.CreaateGenre;

namespace Application.BookOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact] // Fact means this is a test method
        public void WhenAlreadyExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var genre = new Genre()
            {
                Name = "Test_WhenAlreadyExistGenreIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel()
            {
                Name = "Test Genre"
            };
            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre already exists!");
            // assert
        }
    }
}