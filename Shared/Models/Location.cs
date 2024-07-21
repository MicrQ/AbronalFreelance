namespace AbronalFreelance.Shared.Models;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationTypeId { get; set; }
    public LocationType LocationType { get; set; }
    public int? ParentId { get; set; }
    public bool isActive { get; set; }
}
