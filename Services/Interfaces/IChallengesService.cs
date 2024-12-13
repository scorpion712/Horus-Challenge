using Horus_Challenge.Models;

namespace Horus_Challenge.Services.Interfaces;

public interface IChallengesService
{
    Task<IEnumerable<Challenge>?> GetAll();
}
