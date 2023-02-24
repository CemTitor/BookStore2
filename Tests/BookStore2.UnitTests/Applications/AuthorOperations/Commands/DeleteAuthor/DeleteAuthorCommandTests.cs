using AutoMapper;
using BookStore2.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore2.Entities;
using BookStore2.DbOperations;
using FluentAssertions;
using TestSetup;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact] 
        public void WhenAuthorNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 999;
            // act && assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
        {
            // arrange
            var author = new Author()
            {
                Name = "Test_WhenValidInputsAreGiven_Author_ShouldBeDeleted",
                Surname = "Test_WhenValidInputsAreGiven_Author_ShouldBeDeleted",
                BirthDate = DateTime.Now.Date.AddYears(-10)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = author.Id;
            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var deletedAuthor = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);
            deletedAuthor.Should().BeNull();
        }
    }
}