using BookStore2.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentValidation;

namespace BookStore2.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.AuthorId).NotEmpty();
        }
    }
}