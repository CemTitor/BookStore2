using AutoMapper;
using BookStore2.DbOperations;
using BookStore2.Entities;

namespace BookStore2.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GenresViewModel> Handle()
        {
            var _genres = _context.Genres.Where(x => x.isActive == true).OrderBy(x => x.Id).ToList<Genre>();
            List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(_genres);
            return vm;
        }
        public class GenresViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}