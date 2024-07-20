namespace AbronalFreelance.Shared.Models;

public class Location
{
    public int Id { get; set; }
    public int LocationTypeId { get; set; }
    public LocationType LocationType { get; set; }
}
