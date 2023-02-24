using AutoMapper;
using FluentAssertions;
using TestSetup;
using BookStore2.DbOperations;
using BookStore2.Application.GenreOperations.Commands.UpdateGenre;
using BookStore2.Application.BookOperations.Commands.UpdateBook;

namespace Application.GenreOperations.Commands.UpdateBook
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(-99, "Lord Of The Rings")]
        [InlineData(0, "Lord Of The Rings")]
        [InlineData(3, "")]
        [InlineData(-1, "Lord Of The Rings")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int genreId, string name)
        {
            // arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = name
            };
            // act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;
            command.Model = new UpdateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = System.DateTime.Now.Date.AddYears(-1),
                GenreId = 1
            };
            // act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Equals(0);


        }
    }
}