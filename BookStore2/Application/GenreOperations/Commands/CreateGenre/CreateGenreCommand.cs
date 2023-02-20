using AutoMapper;
using BookStore2.DbOperations;
using BookStore2.Entities;

namespace BookStore2.Application.GenreOperations.Commands.CreaateGenre
{
    public class CreaateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public CreaateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
            {
                throw new InvalidOperationException("Genre already exists!");
            }
            genre = new Genre();
            genre.Name = Model.Name;

            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}