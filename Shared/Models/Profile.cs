using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class Profile
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public double AverageRating { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? Picture { get; set; }
    public string? Headline { get; set; }
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}
