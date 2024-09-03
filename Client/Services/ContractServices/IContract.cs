using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ContractServices;

public interface IContract
{
    Task<ContractDTO> CreateContractAsync(string userId, ContractDTO contractDTO);
}
