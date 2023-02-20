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
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}

