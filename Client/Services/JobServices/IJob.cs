using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.JobService;

public interface IJob {
    Task<List<JobDTO>> GetAllJobsAsync(string userId);
    Task<JobDTO> GetJobByIdAsync(string id);
    Task<JobDTO> CreateJobAsync(JobDTO jobDTO);
}