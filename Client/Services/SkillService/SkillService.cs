using System.Net.Http.Json;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.SkillService;

public class SkillService : ISkill
{
    private readonly HttpClient _http;
    private readonly string _url = "api/skills";
    public SkillService(HttpClient http)
    {
        _http = http;
    }
    public async Task<List<Skill>> GetAllSkillsAsync() {
        return await _http.GetFromJsonAsync<List<Skill>>(_url);
    }

}
