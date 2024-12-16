namespace Horus_Challenge.Models;

public class User
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string AuthorizationToken { get; set; } = string.Empty;
}
