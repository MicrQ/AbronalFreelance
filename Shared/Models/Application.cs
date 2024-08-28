using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class Application
{
    public int Id { get; set; }
    public string FreelancerId { get; set; }
    public int JobId { get; set; }
    public string Proposal { get; set; }
    public string DeliveryTime { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public double Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool FavoriteFlag { get; set; } = false;



    [ForeignKey("JobId")]
    public Job Job { get; set; }
    [ForeignKey("FreelancerId")]
    public User Freelancer { get; set; }
}
