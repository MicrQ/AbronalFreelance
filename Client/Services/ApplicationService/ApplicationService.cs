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
    public async Task<ApplicationDTO> CreateApplication(ApplicationDTO appDTO)
    {
        var res = await _http.PostAsJsonAsync("api/application", appDTO);
        return await res.Content.ReadFromJsonAsync<ApplicationDTO>();
    }
}