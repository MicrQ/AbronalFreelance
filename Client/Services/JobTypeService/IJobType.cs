using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.JobTypeService;

public interface IJobType
{
    Task<List<JobType>> GetAllJobTypesAsync();
}
