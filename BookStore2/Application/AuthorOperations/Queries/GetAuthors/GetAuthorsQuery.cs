using AutoMapper;
using BookStore2.DbOperations;
using BookStore2.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore2.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<AuthorsViewModel> Handle()
        {
            var authors = _dbContext.Authors.OrderBy(author => author.Id).ToList<Author>();
            List<AuthorsViewModel> authorsViewModel = _mapper.Map<List<AuthorsViewModel>>(authors);
        
            return authorsViewModel;
        }
    }
    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
    }
}
