namespace AbronalFreelance.Shared.Models;

public class JobStatus
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }
    public DateTime DateTime { get; set; }
    public int ApprovalStatusId { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }
}
