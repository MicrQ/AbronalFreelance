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
        await _http.PutAsJsonAsync("api/client/profile", ClientProfileDTO);
    }

    public async Task UpdateFreelancerProfileAsync(FreelancerProfileDTO FreelancerProfileDTO, string UserId)
    {
        await _http.PutAsJsonAsync(_url_freelancer + UserId, FreelancerProfileDTO);
    }

    public async Task<PasswordDTO> ChangePasswordAsync(string UserId, PasswordDTO PasswordDTO)
    {
        var response = await _http.PutAsJsonAsync("api/user/change-password?UserId=" + UserId, PasswordDTO);
        return await response.Content.ReadFromJsonAsync<PasswordDTO>();
    }

    public async Task<List<FreelancerProfileDTO>> GetTopFreelancersAsync()
    {
        return await _http.GetFromJsonAsync<List<FreelancerProfileDTO>>("api/top-freelancers");
    }

    public async Task<List<FreelancerProfileDTO>> GetTopFreelancersByFilterAsync(
        int? categoryId, int? locationId, double? rating) {
        return await _http.GetFromJsonAsync<List<FreelancerProfileDTO>>("api/top-freelancers/filter?categoryId=" + categoryId + "&locationId=" + locationId + "&rating=" + rating);
    }

    public async Task<InfoDTO> GetInfoAsync(string UserId)
    {
        return await _http.GetFromJsonAsync<InfoDTO>("api/client/" + UserId + "/info");
    }

    public async Task<InfoDTO> GetAppInfoAsync(string UserId)
    {
        return await _http.GetFromJsonAsync<InfoDTO>("api/freelancer/" + UserId + "/info");
    }
}
