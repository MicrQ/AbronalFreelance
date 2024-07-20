namespace AbronalFreelance.Shared.Models;

public class Job
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Budget { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public User CreatedByUser { get; set; }
    public DateTime Deadline { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public int PaymentTypeId { get; set; }
    public PaymentType PaymentType { get; set; }
    public int JobTypeId { get; set; }
    public JobType JobType { get; set; }
}
