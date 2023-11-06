namespace Backend.Models.Users;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string EmailId { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
    public string UserRole { get; set; }
}