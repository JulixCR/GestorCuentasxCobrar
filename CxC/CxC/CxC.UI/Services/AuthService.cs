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
        var user = await _repository.GetUserAsync(username, password);
        if (user is null)
        {
            return null;
        }

        var session = new UserSession(user.IdUsuario, user.Usuario, user.NombreCompleto, "User");
        await _authenticationStateProvider.SignInAsync(session);
        return session;
    }

    public Task SignOutAsync()
    {
        return _authenticationStateProvider.SignOutAsync();
    }

}
