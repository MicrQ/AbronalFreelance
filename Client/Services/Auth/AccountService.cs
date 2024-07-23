using System.Net.Http.Json;
using AbronalFreelance.Client.Services;
using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client;

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

    public async Task<RegisterResponse> RegisterUserAsync(RegisterDTO model)
    {
        var response = await _http.PostAsJsonAsync("api/Auth/register", model);
        var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
        return result!;
    }
}
