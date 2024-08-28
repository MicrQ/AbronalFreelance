using AbronalFreelance.Shared.DTOs;
using System.Net.Http.Json;

namespace AbronalFreelance.Client.Services.JobService;

public class JobService : IJob
{
    private readonly HttpClient _http;

    public JobService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<JobDTO>> GetAllJobsAsync(string userId) {
        return await _http.GetFromJsonAsync<List<JobDTO>>($"api/{userId}/jobs");
    }

    public async Task<JobDTO> GetJobByIdAsync(string id) {
        return await _http.GetFromJsonAsync<JobDTO>($"api/job/{id}");
    }

    public async Task<List<JobDTO>> GetRecentJobsAsync(int? field = null, int? locationId = null, string? title = null, string? userId = null, int limit = 10) {
        string uri = "api/jobs/recent?";
        if (userId != null) {
            uri += $"userId={userId}";
            return await _http.GetFromJsonAsync<List<JobDTO>>(uri);
        }
        if (field != null) uri += $"field={field}&";
        if (locationId != null) uri += $"locationId={locationId}&";
        if (title != null) uri += $"title={title}&";
        if (limit != 10) uri += $"limit={limit}";
        return await _http.GetFromJsonAsync<List<JobDTO>>(uri);
    }

    public async Task<JobDTO> CreateJobAsync(JobDTO jobDTO) {
        var res = await _http.PostAsJsonAsync("api/jobs", jobDTO);
        return await res.Content.ReadFromJsonAsync<JobDTO>();
    }

    public async Task<JobDTO> UpdateJobAsync(string userId, JobDTO jobDTO) {
        var res = await _http.PutAsJsonAsync($"api/job/{jobDTO.Id}?userid={userId}", jobDTO);
        return await res.Content.ReadFromJsonAsync<JobDTO>();
    }

}