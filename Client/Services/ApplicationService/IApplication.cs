using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ApplicationService;

public interface IApplication
{
    Task<ApplicationDTO> CreateApplication(ApplicationDTO appDTO);
    Task<List<ApplicationDTO>> GetAllApplications(string userId, int jobId);
}