using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace AbronalFreelance.Client.Services.LocationServices;

public interface ILocation
{
    Task<List<Location>> GetLocations(string endpoint);
    Task<List<Location>> OnLocationChange(ChangeEventArgs e, string endpoint);
}
