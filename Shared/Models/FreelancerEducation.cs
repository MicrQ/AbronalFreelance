using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class FreelancerEducation
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int? FieldId { get; set; }
    public int EducationLevelId { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }
    [ForeignKey("FieldId")]
    public Field? Field { get; set; }
    [ForeignKey("EducationLevelId")]
    public EducationLevel? EducationLevel { get; set; }
    // public string InstituteName { get; set; }
    // public DateTime StartingDate { get; set; }
    // public DateTime EndDate { get; set; }
}
