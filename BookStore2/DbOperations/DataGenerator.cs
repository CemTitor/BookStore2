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

                context.Genres.AddRange(
                      new Genre
                      {
                          //   Id = 1,
                          Name = "Fantasy",
                      },
                      new Genre
                      {
                          //   Id = 2,
                          Name = "Science Fiction",
                      },
                      new Genre
                      {
                          //   Id = 3,
                          Name = "Romance",
                      },
                      new Genre
                      {
                          //   Id = 4,
                          Name = "Horror",
                      }
                );

                context.Books.AddRange(
                      new Book
                      {
                          //   Id = 1,
                          Title = "The Fountainhead",
                          GenreId = 1,
                          PageCount = 500,
                          PublishDate = DateTime.Now.AddYears(-10),
                      },
                      new Book
                      {
                          //   Id = 2,
                          Title = "Herland",
                          GenreId = 2,
                          PageCount = 250,
                          PublishDate = DateTime.Now.AddYears(-10),
                      },
                      new Book
                      {
                          //   Id = 3,
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
}
