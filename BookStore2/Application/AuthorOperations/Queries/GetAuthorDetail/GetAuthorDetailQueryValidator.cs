using FluentValidation;

namespace BookStore2.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
        }
    }
}
