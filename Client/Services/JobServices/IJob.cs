using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.JobService;

public interface IJob {
    Task<JobDTO> CreateJobAsync(JobDTO jobDTO);
}