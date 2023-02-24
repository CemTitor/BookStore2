using AutoMapper;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;
using BookStore2.Application.BookOperations.Queries.GetBookDetail;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

        }
        [Theory]
        [InlineData(50)]
        [InlineData(0)]
        [InlineData(-10)] 
        public void WhenGivenBookIdIsNotFound_InvalidOperationException_ShouldBeReturn(int id)
        {
            // arrange
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = id;
            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found!");
            // assert
        }
    }
}