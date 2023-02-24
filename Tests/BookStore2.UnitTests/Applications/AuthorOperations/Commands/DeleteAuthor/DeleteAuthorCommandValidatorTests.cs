using AutoMapper;
using BookStore2.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentAssertions;
using TestSetup;
using BookStore2.DbOperations;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteAuthorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Arrange
            DeleteAuthorCommand author = new DeleteAuthorCommand(_context) { AuthorId = 0 };

            // Act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(author);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            DeleteAuthorCommand author = new DeleteAuthorCommand(_context) { AuthorId = 999 };

            // Act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(author);

            // Assert
            result.Errors.Count.Should().Be(0);
        }

    }
}