using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Author
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Imię i nazwisko jest wymagane")]
    [Display(Name = "Autor")]
    public string Name { get; set; }

    public ICollection<Book>? Books { get; set; }
}