using CxC.UI.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace CxC.UI.Services;

public class AuthService
{
    private readonly IUserRepository _repository;
    private readonly CustomAuthenticationStateProvider _authenticationStateProvider;

    public AuthService(IUserRepository repository, AuthenticationStateProvider authenticationStateProvider)
    {
        _repository = repository;
        _authenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
    }

    public async Task<UserSession?> SignInAsync(string username, string password)
    {
        var user = await _repository.GetUserAsync(username);
        if (user is null)
        {
            return null;
        }

        if (!VerifyPassword(password, user.Contrasena))
        {
            return null;
        }

        var session = new UserSession(user.Id, user.Usuario, user.Nombre, "User");
        await _authenticationStateProvider.SignInAsync(session);
        return session;
    }

    public Task SignOutAsync()
    {
        return _authenticationStateProvider.SignOutAsync();
    }

    private static bool VerifyPassword(string providedPassword, string storedPassword)
    {
        return !string.IsNullOrWhiteSpace(storedPassword) && storedPassword.Trim() == providedPassword.Trim();
    }
}
