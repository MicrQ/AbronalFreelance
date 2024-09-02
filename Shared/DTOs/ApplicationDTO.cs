namespace AbronalFreelance.Shared.DTOs;

public class ApplicationDTO {
    public int? Id { get; set; }
    public string? FreelancerId { get; set; }
    public string? FreelancerFullName { get; set; }
    public string? Picture { get; set; }
    public int? JobId { get; set; }
    public string? JobTitle { get; set; }
    public decimal? JobBudget { get; set; }
    public string? Proposal { get; set; }
    public string? DeliveryTime { get; set; }
    public double? Amount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? StatusName { get; set; }
    public bool FavoriteFlag { get; set; } = false;

    public bool Flag { get; set; } = false;
    public string? Message { get; set; }
}