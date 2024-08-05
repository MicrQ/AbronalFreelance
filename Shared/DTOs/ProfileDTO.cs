using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Shared;

public class ProfileDTO
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string? Headline { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public List<FreelancerSkill?>? TopSkills { get; set; }
    // public List<Field>? TopFields { get; set; }
}

