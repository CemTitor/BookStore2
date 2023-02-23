using BookStore2.Entities;
using Microsoft.EntityFrameworkCore;
namespace BookStore2.DbOperations
{
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
                          //   Id = 1,
                          Title = "The Fountainhead",
                          GenreId = 1,
                          PageCount = 500,
                          PublishDate = DateTime.Now.AddYears(-10),
                          AuthorId = 1,


                      },
                      new Book
                      {
                          //   Id = 2,
                          Title = "Herland",
                          GenreId = 2,
                          PageCount = 250,
                          PublishDate = DateTime.Now.AddYears(-10),
                          AuthorId = 2,
                      },
                      new Book
                      {
                          //   Id = 3,
                          Title = "Dune",
                          GenreId = 2,
                          PageCount = 600,
                          PublishDate = DateTime.Now.AddYears(-10),
                          AuthorId = 3,

                      }
                );

                context.Authors.AddRange(
                      new Author
                      {
                          //   Id = 1,
                          Name = "Ayn",
                          Surname = "Rand",
                          BirthDate = DateTime.Now.AddYears(-50),
                      },
                      new Author
                      {
                          //   Id = 2,
                          Name = "Charlotte",
                          Surname = "Perkins Gilman",
                          BirthDate = DateTime.Now.AddYears(-50),
                      },
                      new Author
                      {
                          //   Id = 3,
                          Name = "Frank",
                          Surname = "Herbert",
                          BirthDate = DateTime.Now.AddYears(-50),
                      }
                );

                context.SaveChanges();
            }
        }
    }
}
