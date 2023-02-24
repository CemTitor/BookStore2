using AutoMapper;
using TestSetup;
using BookStore2.DbOperations;
using BookStore2.Application.GenreOperations.Commands.CreaateGenre;
using FluentAssertions;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData("")]
        [InlineData("11")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            // arrange
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel()
            {
                Name = name
            };
            // act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel()
            {
                Name = "Fantasy"
            };
            // act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}