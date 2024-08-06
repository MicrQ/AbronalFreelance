using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.FieldService;

public interface IField
{
    Task<List<Field>> GetAllFieldsAsync();
}
