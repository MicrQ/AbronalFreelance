using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Shared.DTOs;

public class JobDTO {
    public int? Id { get; set; }
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
    public List<Skill>? Skills { get; set; }
    public List<Field>? Fields { get; set; }
    public int? TotalApplications { get; set; }
    public string? Status { get; set; }
    
}
