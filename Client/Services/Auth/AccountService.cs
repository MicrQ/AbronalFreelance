using System.Net.Http.Json;
using System.Security.Claims;
using AbronalFreelance.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace AbronalFreelance.Client.Services.Auth;

public class AccountService : IAccount
{
    private readonly HttpClient _http;
    public AccountService(HttpClient http)
    {
        _http = http;
    }
    public async Task<LoginResponse> LoginUserAsync(LoginDTO model)
    {
        var response = await _http.PostAsJsonAsync("api/Auth/login", model);
        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        return result!;
    }

    public async Task LogoutUserAsync()
    {
        await _http.PostAsync("api/Auth/logout", null);
    }

    public async Task<RegisterResponse> RegisterUserAsync(RegisterDTO model)
    {
        var response = await _http.PostAsJsonAsync("api/Auth/register", model);
        var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
        return result!;
    }

    public Claim GetLoggedInUserIdAsync(AuthenticationState authState){
        var loggedInUser = authState.User.FindFirst(
            c => c.Type == ClaimTypes.NameIdentifier
        );

        return loggedInUser;
    }

}
