using FluentValidation;

namespace BookStore2.Application.BookOperations.Commands.CreateBooks
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
        }
    }
}