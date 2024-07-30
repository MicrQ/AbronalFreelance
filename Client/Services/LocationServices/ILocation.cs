using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Components;
using AbronalFreelance.Shared.DTOs;


namespace AbronalFreelance.Client.Services.LocationServices;

public interface ILocation
{
    Location CurrentLocation { get; set; }

    Task<List<Location>> GetLocations(string endpoint);
    Task<List<Location>> OnLocationChange(int id, string endpoint);
    Task<List<Location>> GetAllLocations();
    Task<Location> GetLocation(int id);
    Task AddLocation(LocationDTO locationDTO);

    Task EditLocation(LocationDTO locationDTO);
    Task DeleteLocation(int id);


    // for location types
    Task<List<LocationType>> GetLocationTypes();
    Task<List<Location>> GetLocationsByType(int id);

}
