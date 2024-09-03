using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ContractServices;

public interface IContract
{
    Task<ContractDTO> CreateContractAsync(string userId, ContractDTO contractDTO);
    Task<List<ContractDTO>> GetContractsByUserIdAsync(string userId);
    Task<List<ContractDTO>> GetFreelancerContractsByUserIdAsync(string userId);
}
