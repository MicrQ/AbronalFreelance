namespace AbronalFreelance.Shared.Models;

public class JobPool
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }
    public string FreelancerId { get; set; }
    public User Freelancer { get; set; }
    public DateTime DateAdded { get; set; }
}
