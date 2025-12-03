using CxC.UI.Models;

namespace CxC.UI.Services;

public interface IUserRepository
{
    Task<UserRecord?> GetUserAsync(string username);
}
