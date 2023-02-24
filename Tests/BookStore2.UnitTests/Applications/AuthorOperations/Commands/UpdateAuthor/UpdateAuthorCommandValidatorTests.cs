using AutoMapper;
using FluentAssertions;
using TestSetup;
using BookStore2.DbOperations;
using BookStore2.Application.AuthorOperations.Commands.UpdateAuthor;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Arrange
            UpdateAuthorCommand author = new UpdateAuthorCommand(_context) { AuthorId = 0 };

            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(author);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateAuthorCommand author = new UpdateAuthorCommand(_context) { AuthorId = 999 };

            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(author);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}