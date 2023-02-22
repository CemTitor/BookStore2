using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore2.Entities
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; } // Foreign Key
        public int AuthorId { get; set; } // Foreign Key
        public bool isActive { get; set; }

        public Genre Genre { get; set; } 
        public Author AuthorOfBook { get; set; }

    }
}

