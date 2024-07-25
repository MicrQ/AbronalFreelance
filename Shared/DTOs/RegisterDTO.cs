using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AbronalFreelance.Shared.DTOs;

public class RegisterDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int LocationId { get; set; }
    public string Role { get; set; }

}
