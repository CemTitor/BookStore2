using AutoMapper;
using BookStore2.DbOperations;

namespace BookStore2.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreID { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var _genre = _context.Genres.SingleOrDefault(x => x.isActive == true && x.Id == GenreID);
            if(_genre is null)
                throw new InvalidOperationException("Genre not found!");
                
            return _mapper.Map<GenreDetailViewModel>(_genre);
        }
        public class GenreDetailViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}