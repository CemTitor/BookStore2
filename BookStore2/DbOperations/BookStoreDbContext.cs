using Microsoft.EntityFrameworkCore;

namespace BookStore2.DbOperations
{
    public class BookStoreDbContext : DbContext
    {
    
        /// We are injecting the DbContextOptions<BookStoreDbContext> into the constructor
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }
        /// We are creating the Books DbSet
        public DbSet<Book> Books { get; set; }
    }
}