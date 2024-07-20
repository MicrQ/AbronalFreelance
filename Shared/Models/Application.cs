namespace AbronalFreelance.Shared.Models;

public class Application
{
    public int Id { get; set; }
    public string FreelancerId { get; set; }
    public User Freelancer { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Proposal { get; set; }
    public bool FavoriteFlag { get; set; }
}
