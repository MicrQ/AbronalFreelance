using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class JobFields
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int FieldId { get; set; }
    [ForeignKey("JobId")]
    public Job? Job { get; set; }
    [ForeignKey("FieldId")]
    public Field? Field { get; set; }
}
