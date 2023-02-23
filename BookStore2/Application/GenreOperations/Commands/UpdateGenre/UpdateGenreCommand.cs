using BookStore2.DbOperations;

namespace BookStore2.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public UpdateGenreModel Model { get; set; }
        public int GenreId { get; set; }
        public UpdateGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {  
                throw new InvalidOperationException("Genre not found!");
            }
            if(_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Genre already exists!");

            genre.Name = Model.Name != default ? Model.Name : genre.Name;
            genre.isActive = Model.isActive;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool isActive { get; set; } = true;
    }
}