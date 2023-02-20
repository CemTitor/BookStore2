using FluentValidation;

namespace BookStore2.Application.GenreOperations.Commands.CreaateGenre
{
    public class CreaateGenreCommandValidator : AbstractValidator<CreaateGenreCommand>
    {
        public CreaateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}