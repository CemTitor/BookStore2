using System.ComponentModel.DataAnnotations;

namespace BookStore2;

public class Book
{
    
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public int GenreId { get; set; }   

    public int PageCount {get; set;}

    public DateTime PublishDate {get; set;}

}
