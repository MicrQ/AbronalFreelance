using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ApplicationService;

public interface IApplication
{
    Task<ApplicationDTO> CreateApplication(ApplicationDTO appDTO);
}