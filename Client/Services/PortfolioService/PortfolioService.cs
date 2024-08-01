using System.Net.Http.Json;
using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;
using AbronalFreelance.Shared.ResponseModels;


namespace AbronalFreelance.Client.Services.PortfolioService;

public class PortfolioService : IPortfolio
{
    private readonly HttpClient _http;
    private readonly string url = "api/portfolio";
    public PortfolioService(HttpClient http)
    {
        _http = http;
    }


    // get user portfolios
    public async Task<List<FreelancerPortfolio>>? GetFreelancerPortfolios(string UserId)
    {
        return await _http.GetFromJsonAsync<List<FreelancerPortfolio>>(url + "?UserId=" + UserId);
    }


    // add portfolio
    public async Task AddPortfolio(PortfolioDTO portfolioDTO) {
        await _http.PostAsJsonAsync(url, portfolioDTO);
    }


    // delete user portfolio
    public async Task DeletePortfolio(int id, string? UserId) {
        await _http.DeleteAsync(url + $"/{id}?userid={UserId}");
    }
}
