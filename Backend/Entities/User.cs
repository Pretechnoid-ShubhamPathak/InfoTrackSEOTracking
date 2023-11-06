namespace Backend.Entities;

using System.Text.Json.Serialization;
using Authorization;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailId { get; set; }
    public string Username { get; set; }
    public string UserRole { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; }
}