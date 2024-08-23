using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.PaymentTypeService;

public interface IPaymentType
{
    Task<List<PaymentType>> GetAllPaymentTypesAsync();
}
