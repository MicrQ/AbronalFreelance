using System.Security.Claims;
using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace AbronalFreelance.Client.Services.Auth;

public interface IAccount
{
    Task<RegisterResponse> RegisterUserAsync(RegisterDTO model);
    Task<LoginResponse> LoginUserAsync(LoginDTO model);
    Task LogoutUserAsync();
    Claim GetLoggedInUserIdAsync(AuthenticationState authState);
}
