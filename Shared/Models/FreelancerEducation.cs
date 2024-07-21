namespace AbronalFreelance.Shared.Models;

public class FreelancerEducation
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public Field Field { get; set; }
    public EducationLevel EducationLevel { get; set; }
    public string InstituteName { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndDate { get; set; }
}
