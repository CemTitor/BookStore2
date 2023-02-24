using AutoMapper;
using BookStore2.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using TestSetup;
using BookStore2.DbOperations;


namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel()
            {
                Name = "Test_WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                Surname = "Test_WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                BirthDate = System.DateTime.Now.Date.AddYears(-1)
            };
            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author already exists!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel()
            {
                Name = "Test_WhenValidInputsAreGiven_Author_ShouldBeCreated",
                Surname = "Test_WhenValidInputsAreGiven_Author_ShouldBeCreated",
                BirthDate = System.DateTime.Now.Date.AddYears(-10)
            };
            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Name == command.Model.Name);
            author.Should().NotBeNull();
            author.Surname.Should().Be(command.Model.Surname);
            author.BirthDate.Should().Be(command.Model.BirthDate);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel();
            // act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }                                                           
    }
}