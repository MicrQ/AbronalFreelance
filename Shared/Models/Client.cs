namespace AbronalFreelance.Shared.Models;

public class Client
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public string CompanyName { get; set; }
    public int LocationId { get; set; }
    public string TinNo { get; set; }
    public DateTime CompanyEstablishedAt { get; set; }
}
