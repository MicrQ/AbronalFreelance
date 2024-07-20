using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Shared.Models;

public class FreelancerPool
{
    public int Id { get; set; }
    public string ClientId { get; set; }
    public User Client { get; set; }
    public string FreelancerId { get; set; }
    public User Freelancer { get; set; }
    public DateTime DateAdded { get; set; }
}
