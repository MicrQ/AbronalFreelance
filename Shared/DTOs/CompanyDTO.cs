namespace AbronalFreelance.Shared.DTOs;

public class CompanyDTO
{
    public string UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? TinNo { get; set; }
    public int LocationId { get; set; }
    public string? LocationString { get; set; }
    public DateTime? EstablishedDate { get; set; }
    public bool Flag { get; set; } = true;
    public string? Message { get; set; }
}
