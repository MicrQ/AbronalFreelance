using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class JobStatus
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int ApprovalStatusId { get; set; }
    public DateTime DateTime { get; set; }
    [ForeignKey("JobId")]
    public Job Job { get; set; }
    [ForeignKey("ApprovalStatusId")]
    public ApprovalStatus ApprovalStatus { get; set; }
}
