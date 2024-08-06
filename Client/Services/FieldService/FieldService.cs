using System.Net.Http.Json;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.FieldService;

public class FieldService : IField
{
    private readonly HttpClient _http;
    public FieldService(HttpClient http)
    {
        _http = http;
    }
    public async Task<List<Field>> GetAllFieldsAsync()
    {
        var fields = await _http.GetFromJsonAsync<List<Field>>("api/fields");
        return fields != null ? fields : new List<Field>();
    }
}
