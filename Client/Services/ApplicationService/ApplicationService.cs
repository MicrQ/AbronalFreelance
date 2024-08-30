using System.Net.Http.Json;
using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ApplicationService;

public class ApplicationService : IApplication
{
    private readonly HttpClient _http;
    public ApplicationService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ApplicationDTO> GetApplicationByIdAsync(string userId, int appId) {
        return await _http.GetFromJsonAsync<ApplicationDTO>($"api/application/{appId}?userid={userId}");
    }


    public async Task<List<ApplicationDTO>> GetAllApplications(string userId, int jobId)
    {
        return await _http.GetFromJsonAsync<List<ApplicationDTO>>($"api/job/{jobId}/applications?userid={userId}");
    }

    public async Task<ApplicationDTO> CreateApplication(ApplicationDTO appDTO)
    {
        var res = await _http.PostAsJsonAsync("api/application", appDTO);
        return await res.Content.ReadFromJsonAsync<ApplicationDTO>();
    }

}