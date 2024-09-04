using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ProfileService;

public interface IProfile
{
    Task<FreelancerProfileDTO> GetFreelancerProfileAsync(string UserId);
    Task<ClientProfileDTO> GetClientProfileAsync(string UserId);
    Task UpdateFreelancerProfileAsync(FreelancerProfileDTO FreelancerProfileDTO, string UserId); //return response object...maybe
    Task UpdateClientProfileAsync(ClientProfileDTO ClientProfileDTO);
    Task<PasswordDTO> ChangePasswordAsync(string UserId, PasswordDTO PasswordDTO);

    Task<List<FreelancerProfileDTO>> GetTopFreelancersAsync();
}
