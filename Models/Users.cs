using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class User
{
    public int Id { get; set; }
    [Required] public string Login { get; set; }
    [Required] public string PasswordHash { get; set; } // Tu trafi skrót hasła
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required, EmailAddress] public string Email { get; set; }
}