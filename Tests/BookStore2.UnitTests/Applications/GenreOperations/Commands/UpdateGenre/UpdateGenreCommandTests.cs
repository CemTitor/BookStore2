using AutoMapper;
using BookStore2.Application.GenreOperations.Commands.UpdateGenre;
using BookStore2.Entities;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

        }
        [Theory]
        [InlineData(50)]
        [InlineData(0)]
        [InlineData(-10)] // Fact means this is a test method
        public void WhenGivenGenreIdIsNotValid_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            // arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = "Test Genre"
            };
            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not found!");
            // assert
        }


    }
}