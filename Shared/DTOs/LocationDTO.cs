namespace AbronalFreelance.Shared.DTOs;

public class LocationDTO
{
    public string Name { get; set; }
    public int LocationTypeId { get; set; } 
    public int? ParentId { get; set; }
    public bool isActive { get; set; } = true;
}
