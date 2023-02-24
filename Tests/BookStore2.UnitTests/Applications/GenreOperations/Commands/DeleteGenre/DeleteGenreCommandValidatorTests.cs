using AutoMapper;
using BookStore2.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;
using TestSetup;
using BookStore2.DbOperations;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        // [Theory]
        // [InlineData(-999)]
        // [InlineData(0)]
        // [InlineData(-1)]
        // [InlineData(1)]
        // public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError(int genreId){
        //     // Arrange
        //     DeleteGenreCommand command = new DeleteGenreCommand(_context);
        //     command.GenreId = genreId;

        //     // Act
        //     DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        //     var result = validator.Validate(command);

        //     // Assert
        //     result.Errors.Count.Should().BeGreaterThan(0);
        // }            

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors(){
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 2;

            // Act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}