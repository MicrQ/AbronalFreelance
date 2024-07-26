using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.Auth;

public interface IAccount
{
    Task<RegisterResponse> RegisterUserAsync(RegisterDTO model);
    Task<LoginResponse> LoginUserAsync(LoginDTO model);
    Task LogoutUserAsync();
}
