using AutoMapper;
using BookStore2.Application.BookOperations.Commands.DeleteBook;
using FluentAssertions;
using TestSetup;
using BookStore2.DbOperations;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteBookCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(-999)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError(int bookId)
        {
            // Arrange
            DeleteBookCommand book = new DeleteBookCommand() { BookId = bookId };

            // Act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            var result = validator.Validate(book);
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            DeleteBookCommand book = new DeleteBookCommand() { BookId = 999 };

            // Act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(book);

            // Assert
            result.Errors.Count.Should().Be(0);
        }


    }
}