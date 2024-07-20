namespace AbronalFreelance.Shared.Models;

public class LocationType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SysCode { get; set; }
    public int? ParentId { get; set; }
    public bool IsActive { get; set; }
}
