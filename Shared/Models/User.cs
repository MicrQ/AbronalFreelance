using Microsoft.AspNetCore.Identity;

namespace AbronalFreelance.Shared.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int LocationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
