namespace AbronalFreelance.Shared.Models;

public class ApplicationStatus
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public Application Application { get; set; }
    public DateTime DateTime { get; set; }
    public int ApprovalStatusId { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }
}
