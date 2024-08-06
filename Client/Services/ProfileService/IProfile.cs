using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ProfileService;

public interface IProfile
{
    Task<ProfileDTO> GetUserProfileAsync(string UserId);
    Task UpdateProfileAsync(ProfileDTO profileDTO, string UserId); //return response object...maybe
}
