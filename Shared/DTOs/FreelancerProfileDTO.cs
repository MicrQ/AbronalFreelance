using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Shared.DTOs;

public class FreelancerProfileDTO
{
    public string? UserId { get; set; }
    public double AverageRating { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Headline { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Location { get; set; }
    public int LocationId { get; set; }
    public string? FreelancerFields { get; set; }
    public string? FreelancerSkills { get; set; }
    // educatioon and other info's to be added in another sidebaar tab || here probably

    public List<FreelancerSkill>? TopSkills { get; set; }
    public List<FreelancerField>? TopFields { get; set; }
    public bool Flag { get; set; } = false;
    public string? Message { get; set; }
}
