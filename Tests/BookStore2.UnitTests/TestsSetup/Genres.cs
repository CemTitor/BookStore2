using BookStore2.DbOperations;
using BookStore2.Entities;

namespace TestSetup{
    public static class Genres{
        public static void AddGenres(this BookStoreDbContext context){
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
        }
    }
}