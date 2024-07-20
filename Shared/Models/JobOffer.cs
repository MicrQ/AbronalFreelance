using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Shared.Models;

public class JobOffer
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }
    public string FreelancerId { get; set; }
    public User Freelancer { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsRejected { get; set; }
}
