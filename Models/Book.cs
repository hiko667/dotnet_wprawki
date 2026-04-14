using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tytuł jest obowiązkowy")]
    public string Title { get; set; }

    [Display(Name = "Autor")]
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}