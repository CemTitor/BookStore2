using AutoMapper;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;
using BookStore2.Application.GenreOperations.Queries.GetGenreDetail;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

        }
        [Theory]
        [InlineData(50)]
        [InlineData(0)]
        [InlineData(-10)] 
        public void WhenGivenGenreIdIsNotFound_InvalidOperationException_ShouldBeReturn(int id)
        {
            // arrange
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreID = id;
            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not found!");
            // assert
        }
    }
}