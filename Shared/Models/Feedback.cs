namespace AbronalFreelance.Shared.Models;

public class Feedback
{
    public int Id { get; set; }
    public int ContractId { get; set; }
    public Contract Contract { get; set; }
    public string FromUserId { get; set; }
    public User FromUser { get; set; }
    public string ToUserId { get; set; }
    public User ToUser { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
