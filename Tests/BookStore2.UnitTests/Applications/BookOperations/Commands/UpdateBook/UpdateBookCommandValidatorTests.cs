using AutoMapper;
using FluentAssertions;
using TestSetup;
using BookStore2.DbOperations;
using BookStore2.Application.BookOperations.Commands.UpdateBook;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(-99, "Lord Of The Rings")]
        [InlineData(0, "Lord Of The Rings")]
        [InlineData(3, "")]
        [InlineData(-1, "Lord Of The Rings")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title)
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookId;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                PageCount = 100,
                PublishDate = System.DateTime.Now.Date.AddYears(-1),
                GenreId = 1
            };
            // act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}