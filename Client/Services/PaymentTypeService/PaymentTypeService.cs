using System.Net.Http.Json;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.PaymentTypeService;

public class PaymentTypeService : IPaymentType
{
    private readonly HttpClient _http;
    public PaymentTypeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PaymentType>> GetAllPaymentTypesAsync() {
        return await _http.GetFromJsonAsync<List<PaymentType>>("api/payment-types");
    }
}