using Horus_Challenge.Models;

namespace Horus_Challenge.Services.Interfaces;

public interface IAuthService
{
    Task<User?> SignIn(string username, string password);
}
