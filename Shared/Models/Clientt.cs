using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class Clientt
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string CompanyName { get; set; }
    public int CompanyLocationId { get; set; }
    public DateTime? CompanyEstablishedAt { get; set; }
    public string? TinNo { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; } = null;
    [ForeignKey("CompanyLocationId")]
    public Location? CompanyLocation { get; set; } = null;
}
