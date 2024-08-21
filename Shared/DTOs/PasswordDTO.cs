namespace AbronalFreelance.Shared.DTOs;

public class PasswordDTO {
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public bool Flag { get; set; } = false;
    public string? Message { get; set; }
}