using System.Net.Http.Json;
using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ProfileService;

public class ProfileService : IProfile
{
    private readonly HttpClient _http;
    private readonly string _url_freelancer = "api/freelancer/profile?UserId=";
    private readonly string _url_client = "api/client/profile?UserId=";
    public ProfileService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ClientProfileDTO> GetClientProfileAsync(string UserId)
    {
        var clientProfile = await _http.GetFromJsonAsync<ClientProfileDTO>(_url_client + UserId);
        return clientProfile;
    }

    public async Task<FreelancerProfileDTO> GetFreelancerProfileAsync(string UserId)
    {
        FreelancerProfileDTO FreelancerProfileDTO = await _http.GetFromJsonAsync<FreelancerProfileDTO>(_url_freelancer + UserId);
        return FreelancerProfileDTO;
    }

    public async Task UpdateClientProfileAsync(ClientProfileDTO ClientProfileDTO)
    {
        await _http.PutAsJsonAsync(_url_client, ClientProfileDTO);
    }

    public async Task UpdateFreelancerProfileAsync(FreelancerProfileDTO FreelancerProfileDTO, string UserId)
    {
        await _http.PutAsJsonAsync(_url_freelancer + UserId, FreelancerProfileDTO);
    }
}
