using System.Net.Http.Json;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.JobTypeService;

public class JobTypeService : IJobType
{
    private readonly HttpClient _http;
    public JobTypeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<JobType>> GetAllJobTypesAsync() {
        return await _http.GetFromJsonAsync<List<JobType>>("api/job-types");
    }
}