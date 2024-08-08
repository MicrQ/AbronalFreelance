using System.Net.Http.Json;
using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.CompanyService;

public class CompanyService : ICompany
{
    private readonly HttpClient _http;
    private const string _url = "api/company?userid=";
    public CompanyService(HttpClient http)
    {
        _http = http;
    }
    public async Task<CompanyDTO>? GetCompany(string userId)
    {
        return await _http.GetFromJsonAsync<CompanyDTO>(_url + userId);
    }

    public async Task UpdateCompany(CompanyDTO companyDTO, string userId)
    {
        await _http.PutAsJsonAsync(_url + userId, companyDTO);
    }
}
