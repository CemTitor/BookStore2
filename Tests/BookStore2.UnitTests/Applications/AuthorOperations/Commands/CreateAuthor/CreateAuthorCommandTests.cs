using AutoMapper;
using BookStore2.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore2.Entities;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var author = new Author()
            {
                Name = "Test_WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                Surname = "Test_WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                BirthDate = DateTime.Now.Date.AddYears(-1)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand authorModel = new CreateAuthorCommand(_context, _mapper);
            authorModel.Model = new CreateAuthorModel()
            {
                Name = author.Name,
                Surname = author.Surname,
                BirthDate = author.BirthDate
            };
            // Act && Assert
            FluentActions.Invoking(() => authorModel.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Author already exists!");

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
                BirthDate = DateTime.Now.Date.AddYears(-10)
            };
            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var author = _context.Authors.SingleOrDefault(x => x.Name == command.Model.Name);
            author.Should().NotBeNull();
            author.Name.Should().Be(command.Model.Name);
            author.Surname.Should().Be(command.Model.Surname);
            author.BirthDate.Should().Be(command.Model.BirthDate);
        }
    }
}