using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class Job
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [Column(TypeName = "Decimal(18,2)")]
    public decimal? Budget { get; set; }
    public string Duration { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string UserId { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsClosed { get; set; } = false;
    public int LocationId { get; set; }
    public int PaymentTypeId { get; set; }
    public int JobTypeId { get; set; }
    [ForeignKey("JobTypeId")]
    public JobType JobType { get; set; }
    [ForeignKey("PaymentTypeId")]
    public PaymentType PaymentType { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    [ForeignKey("LocationId")]
    public Location Location { get; set; }
}
