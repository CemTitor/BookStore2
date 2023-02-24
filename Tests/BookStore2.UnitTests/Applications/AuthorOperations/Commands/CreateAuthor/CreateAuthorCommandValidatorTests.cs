using FluentAssertions;
using TestSetup;
using BookStore2.Application.AuthorOperations.Commands;
using BookStore2.Application.AuthorOperations.Commands.CreateAuthor;

namespace Application.AuthorOperations.Commands;

public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "tes")]
    [InlineData("tes", "tes")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string surname)
    {
        // Arrange
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel { Name = name, Surname = surname };

        // Act
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        // Assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}