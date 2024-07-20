namespace AbronalFreelance.Shared.Models;

public class FreelancerEducation
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int FieldId { get; set; }
    public int EducationLevelId { get; set; }
    public string InstituteName { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndDate { get; set; }
}
