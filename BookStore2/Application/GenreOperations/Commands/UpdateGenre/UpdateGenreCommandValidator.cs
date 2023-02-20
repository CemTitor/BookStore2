using FluentValidation;

namespace BookStore2.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(command => command.Model.Name.Trim() != string.Empty);
            RuleFor(command => command.GenreId).GreaterThan(0);
        }
    }
}
