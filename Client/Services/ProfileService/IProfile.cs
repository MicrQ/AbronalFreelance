using AbronalFreelance.Client.Pages;
using AbronalFreelance.Shared;
using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ProfileService;

public interface IProfile
{
    Task<ProfileDTO> GetUserProfileAsync(string UserId);
}
