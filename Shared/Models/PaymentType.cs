namespace AbronalFreelance.Shared.Models;

public class PaymentType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
