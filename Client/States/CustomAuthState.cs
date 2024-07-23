using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;



namespace AbronalFreelance.Client.State;

public class CustomAuthState : AuthenticationStateProvider
{
    private const string _tokenKey = "auth";
    private readonly ILocalStorageService _localStorage;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthState(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = await _localStorage.GetItemAsStringAsync(_tokenKey);
        if (string.IsNullOrWhiteSpace(token))
            return await Task.FromResult(new AuthenticationState(_anonymous));
        var(role, email) = GetClaims(token);
        if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(email))
            return await Task.FromResult(new AuthenticationState(_anonymous));

        var claims = SetClaimPrincipal(role, email);
        if (claims is null)
            return await Task.FromResult(new AuthenticationState(_anonymous));
        else
            return await Task.FromResult(new AuthenticationState(claims));
    }


    public static ClaimsPrincipal SetClaimPrincipal(string role, string email)
    {
        if (role is null || email is null) return new ClaimsPrincipal();
            
        return new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Role, role!),
            new Claim(ClaimTypes.Email, email!)
        }, "JwtAuth"));
    }
    


    private (string, string) GetClaims(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken)) return (null!, null!);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);

        var role = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)!.Value;
        var email = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)!.Value;

        return (role, email);
    }

    public async Task updateAuthenticationState(string jwtToken) {
        var claims = new ClaimsPrincipal();
        if (!string.IsNullOrEmpty(jwtToken)) {
            var (role, email) = GetClaims(jwtToken);
            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(email))
                return;
            var setClaims = SetClaimPrincipal(role, email);
            if (setClaims is null) return;

            await _localStorage.SetItemAsStringAsync(_tokenKey, jwtToken);
        }
        else
            await _localStorage.RemoveItemAsync(_tokenKey);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
