using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class SkillsForJob
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int SkillId { get; set; }
    [ForeignKey("JobId")]
    public Job? Job { get; set; }
    [ForeignKey("SkillId")]
    public Skill? Skill { get; set; }
}
