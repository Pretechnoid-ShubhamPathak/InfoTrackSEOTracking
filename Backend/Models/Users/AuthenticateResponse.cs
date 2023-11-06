namespace Backend.Models.Users;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailId { get; set; }
    public string UserRole { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
}