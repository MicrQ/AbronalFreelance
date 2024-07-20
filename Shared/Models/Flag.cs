namespace AbronalFreelance.Shared.Models;

public class Flag
{
    public int Id { get; set; }
    public string ReporterId { get; set; }
    public User Reporter { get; set; }
    public string ReportedUserId { get; set; }
    public User ReportedUser { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSeen { get; set; }
    public bool IsResolved { get; set; }
}
