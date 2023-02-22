using AutoMapper;
using BookStore2.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore2.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public GetAuthorDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle()
        {
            var author = _dbContext.Authors.Where(x => x.Id == AuthorId).SingleOrDefault();
            if (author is null)
            {
                throw new InvalidOperationException("Author not found!");
            }
            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
            return vm;
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        // public List<BookViewModel> Books { get; set; }
    }
}