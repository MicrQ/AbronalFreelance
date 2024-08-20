using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ProfileService;

public interface IProfile
{
    Task<FreelancerProfileDTO> GetUserProfileAsync(string UserId);
    Task UpdateProfileAsync(FreelancerProfileDTO FreelancerProfileDTO, string UserId); //return response object...maybe
}
