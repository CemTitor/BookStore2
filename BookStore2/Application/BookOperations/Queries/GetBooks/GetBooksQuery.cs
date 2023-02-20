using AutoMapper;
using BookStore2.DbOperations;
using BookStore2.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore2.Application.BookOperations.Queries.GetBooks
{
    public class GetbooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetbooksQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            var books = _dbContext.Books.Include(x => x.Genre).OrderBy(book => book.Id).ToList<Book>();
            List<BooksViewModel> booksViewModel = _mapper.Map<List<BooksViewModel>>(books);
            // new List<BooksViewModel>();

            // if (books.Count == 0)
            // {
            //     throw new InvalidOperationException("There is no book in the database!");
            // }
            // foreach (var book in books)
            // {
            //     booksViewModel.Add(new BooksViewModel
            //     {
            //         Title = book.Title,
            //         Genre = ((GenreEnum)book.GenreId).ToString(),
            //         PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
            //         PageCount = book.PageCount,
            //     });
            // }
            return booksViewModel;
        }
    }
    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }


    }
}
