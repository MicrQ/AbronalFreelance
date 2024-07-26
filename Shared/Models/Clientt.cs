namespace AbronalFreelance.Shared.Models;

public class Clientt
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User? User { get; set; } = null;
    public string CompanyName { get; set; }
    public int CompanyLocationId { get; set; }
    public Location? CompanyLocation { get; set; } = null;
    public string TinNo { get; set; }
    public DateTime CompanyEstablishedAt { get; set; }
}
