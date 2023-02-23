using BookStore2.DbOperations;
using BookStore2.Entities;

namespace TestSetup{
    public static class Books{
        public static void AddBooks(this BookStoreDbContext context){
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
        }
    }
}