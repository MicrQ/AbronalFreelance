using System.Net.Http.Json;
using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ContractServices;

public class ContractService : IContract
{
    private readonly HttpClient _http;
    public ContractService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ContractDTO> CreateContractAsync(string userId, ContractDTO contractDTO) {
        var res = await _http.PostAsJsonAsync($"api/contract?userId={userId}", contractDTO);
        return await res.Content.ReadFromJsonAsync<ContractDTO>();
    }

    public async Task<List<ContractDTO>> GetContractsByUserIdAsync(string userId) {
        return await _http.GetFromJsonAsync<List<ContractDTO>>($"api/user/{userId}/contracts");
    }
}
