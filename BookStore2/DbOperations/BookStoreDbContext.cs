using BookStore2.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore2.DbOperations
{
    public class BookStoreDbContext :  DbContext, IBookStoreDbContext
    {
        /// We are injecting the DbContextOptions<BookStoreDbContext> into the constructor
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {}
        /// We are creating the Books DbSet
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        public override int SaveChanges() 
        {
            return base.SaveChanges();
        }
    }
}