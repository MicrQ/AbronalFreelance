using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.JobService;

public interface IJob {
    Task<List<JobDTO>> GetAllJobsAsync(string userId);
    Task<JobDTO> GetJobByIdAsync(string id);
    Task<List<JobDTO>> GetRecentJobsAsync(int? field = null, string? userId = null, int limit = 10);
    Task<JobDTO> CreateJobAsync(JobDTO jobDTO);
    Task<JobDTO> UpdateJobAsync(string userId, JobDTO jobDTO);
}