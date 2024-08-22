namespace AbronalFreelacner.Shared.DTOs;

public class JobDTO {
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    public string? Duration { get; set; }
    public string? UserId { get; set; }
    public DateTime? Deadline { get; set; }
    public int? LocationId { get; set; }
    public int? PaymentTypeId { get; set; }
    public int? JobTypeId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool Flag { get; set; } = false;
    public string? Message { get; set; }
}