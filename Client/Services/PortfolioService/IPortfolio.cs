using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;
using AbronalFreelance.Shared.ResponseModels;

namespace AbronalFreelance.Client.Services.PortfolioService;

public interface IPortfolio
{
    Task<List<FreelancerPortfolio>>? GetFreelancerPortfolios(string UserId);
    Task DeletePortfolio(int id, string? UserId);
    Task AddPortfolio(PortfolioDTO portfolioDTO);
}
