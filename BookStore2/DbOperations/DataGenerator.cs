using Microsoft.EntityFrameworkCore;
using BookStore2.DbOperations;
using BookStore2;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {

        using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if (context.Books.Any())
            {
                return;
            }

            context.Books.AddRange(
                  new Book
                  {
                      Id = 1,
                      Title = "The Fountainhead",
                      GenreId = 1,
                      PageCount = 500,
                      PublishDate = DateTime.Now.AddYears(-10),
                  },
                  new Book
                  {
                      Id = 2,
                      Title = "Herland",
                      GenreId = 2,
                      PageCount = 250,
                      PublishDate = DateTime.Now.AddYears(-10),
                  },
                  new Book
                  {
                      Id = 3,
                      Title = "Dune",
                      GenreId = 2,
                      PageCount = 600,
                      PublishDate = DateTime.Now.AddYears(-10),
                  }
            );

            context.SaveChanges();

        }

    }
}