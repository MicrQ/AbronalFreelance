using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ProfileService;

public interface IProfile
{
    void AddCompanyAsync(CompanyInfoDTO companyInfo);
}
