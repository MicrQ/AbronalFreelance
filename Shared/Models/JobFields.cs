namespace AbronalFreelance.Shared.Models;

public class JobFields
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }
    public int FieldId { get; set; }
    public Field Field { get; set; }
}
