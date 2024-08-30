using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.ApplicationService;

public interface IApplication
{
    Task<ApplicationDTO> GetApplicationByIdAsync(string userId, int appId);
    Task<List<ApplicationDTO>> GetAllApplications(string userId, int jobId);
    Task<ApplicationDTO> CreateApplication(ApplicationDTO appDTO);
}