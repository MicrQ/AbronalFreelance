namespace AbronalFreelance.Shared.DTOs;

public class ContractDTO
{
    public int? Id { get; set; }
    public int? ApplicationId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Flag { get; set; } = false;
    public string? Message { get; set; }
}
