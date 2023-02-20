using FluentValidation;

namespace BookStore2.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(command => command.GenreID).GreaterThan(0);
        }
    }
}
