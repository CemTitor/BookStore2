using BookStore2;
using BookStore2.Common;
using BookStore2.DbOperations;

public class GetbooksQuery
{

    private readonly BookStoreDbContext _dbContext;

    public GetbooksQuery(BookStoreDbContext context)
    {
        _dbContext = context;
    }
    
    public List<BooksViewModel> Handle()
    {
        var books = _dbContext.Books.OrderBy(book => book.Id).ToList<Book>();
        List<BooksViewModel> booksViewModel = new List<BooksViewModel>();
        foreach (var book in books)
        {
            booksViewModel.Add(new BooksViewModel
            {
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                PageCount = book.PageCount,
            });
        }
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