using System.Net.Http.Json;
using AbronalFreelance.Shared.DTOs;


namespace AbronalFreelance.Client.Services.ProfileService;

public class ProfileService : IProfile
{
    private readonly HttpClient _http;
    public ProfileService(HttpClient http)
    {
        _http = http;
    }
    public async Task AddCompanyAsync(CompanyInfoDTO companyInfo)
    {
        // will be changed to a Task function and will have a response object
        await _http.PostAsJsonAsync("api/company", companyInfo);
    }
}
