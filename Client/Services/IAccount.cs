using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services;

public interface IAccount
{
    Task<RegisterResponse> RegisterUserAsync(RegisterDTO model);
    Task<LoginResponse> LoginUserAsync(LoginDTO model);
}
