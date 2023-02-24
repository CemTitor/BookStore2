using AutoMapper;
using FluentAssertions;
using TestSetup;
using BookStore2.DbOperations;
using BookStore2.Application.GenreOperations.Queries.GetGenreDetail;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailValidatorQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailValidatorQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(50)]
        [InlineData(0)]
        [InlineData(-10)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            // arrange
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreID = genreId;

            // act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
            
        }
    }
}