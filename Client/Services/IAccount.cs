using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services;

public interface IAccount
{
    Task<RegisterResponse> RegisterUserAsync(RegisterDTO model);
    Task<LoginResponse> LoginUserAsync(LoginDTO model);
    Task LogoutUserAsync();
    Task<List<Location>> GetLocations(string endpoint);
}
