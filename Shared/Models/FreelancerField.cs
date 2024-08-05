using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class FreelancerField
{
    public int Id { get; set; }
    public int FieldId { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    [ForeignKey("FieldId")]
    public virtual Field Field { get; set; }
}
