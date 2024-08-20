using System.Net.Http.Json;
using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ProfileService;

public class ProfileService : IProfile
{
    private readonly HttpClient _http;
    private readonly string _url = "api/user/profile?UserId=";
    public ProfileService(HttpClient http)
    {
        _http = http;
    }

    public async Task<FreelancerProfileDTO> GetUserProfileAsync(string UserId)
    {
        FreelancerProfileDTO FreelancerProfileDTO = await _http.GetFromJsonAsync<FreelancerProfileDTO>(_url + UserId);
        return FreelancerProfileDTO;
    }

    public async Task UpdateProfileAsync(FreelancerProfileDTO FreelancerProfileDTO, string UserId)
    {
        await _http.PutAsJsonAsync(_url + UserId, FreelancerProfileDTO);
    }
}
