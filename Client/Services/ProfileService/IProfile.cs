using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ProfileService;

public interface IProfile
{
    Task AddCompanyAsync(CompanyInfoDTO companyInfo);
}
