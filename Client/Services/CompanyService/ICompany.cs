using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.CompanyService;

public interface ICompany
{
    Task<CompanyDTO> GetCompany(string userId);
    Task UpdateCompany(CompanyDTO companyDTO, string userId);
}
