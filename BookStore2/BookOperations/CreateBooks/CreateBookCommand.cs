using BookStore2;
using BookStore2.DbOperations;

public class CreateBookCommand
{

    public CreateBookModel NewBook { get; set; }
    private readonly BookStoreDbContext _dbContext;
    public CreateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == NewBook.Title);
        if (book is not null)
        {
            throw new InvalidOperationException("Book already exists!");
        }
        book = new Book
        {
            Title = NewBook.Title,
            GenreId = NewBook.GenreId,
            PageCount = NewBook.PageCount,
            PublishDate = NewBook.PublishDate.Date
        };
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }

    
}

public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }