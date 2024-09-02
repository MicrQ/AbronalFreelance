namespace AbronalFreelance.Shared.Models;

public class Contract
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public Application Application { get; set; }
}
