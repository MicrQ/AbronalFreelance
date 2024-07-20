namespace AbronalFreelance.Shared.Models;

public class FreelancerPortfolio
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public DateTime DateAdded { get; set; }
}
