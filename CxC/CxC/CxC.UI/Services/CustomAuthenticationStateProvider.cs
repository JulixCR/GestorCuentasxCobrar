using System.Security.Claims;
using CxC.UI.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CxC.UI.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string SessionKey = "authUser";
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var storedUser = await _sessionStorage.GetAsync<UserSession>(SessionKey);
            if (!storedUser.Success || storedUser.Value is null)
            {
                return new AuthenticationState(_anonymous);
            }

            var claimsPrincipal = CreateClaimsPrincipal(storedUser.Value);
            return new AuthenticationState(claimsPrincipal);
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task SignInAsync(UserSession userSession)
    {
        await _sessionStorage.SetAsync(SessionKey, userSession);
        var claimsPrincipal = CreateClaimsPrincipal(userSession);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task SignOutAsync()
    {
        await _sessionStorage.DeleteAsync(SessionKey);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private static ClaimsPrincipal CreateClaimsPrincipal(UserSession user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.GivenName, user.Name),
            new(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, nameof(CustomAuthenticationStateProvider));
        return new ClaimsPrincipal(identity);
    }
}
