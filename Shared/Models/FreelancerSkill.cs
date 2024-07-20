using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Shared.Models;

public class FreelancerSkill
{
    public int Id { get; set; }
    public int SkillId { get; set; }
    public Skill Skill { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int YearsOfExperience { get; set; }
    public string skilledAt { get; set; }
    public string Description { get; set; }
}
