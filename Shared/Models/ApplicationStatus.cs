using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class ApplicationStatus
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public int ApprovalStatusId { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;

    [ForeignKey("ApplicationId")]
    public Application Application { get; set; }
    [ForeignKey("ApprovalStatusId")]
    public ApprovalStatus ApprovalStatus { get; set; }
}
