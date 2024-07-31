using System.Net.Http.Json;
using AbronalFreelance.Shared.Models;
using AbronalFreelance.Shared.DTOs;


namespace AbronalFreelance.Client.Services.LocationServices;

public class LocationService : ILocation
{
    private readonly HttpClient _http;

    public LocationService(HttpClient http)
    {
        _http = http;
    }
    public Location CurrentLocation { get; set; }


    public async Task<List<Location>> GetLocations(string endpoint)
    {
        return await _http.GetFromJsonAsync<List<Location>>("api/location/" + endpoint);
    }

    public async Task<List<Location>> OnLocationChange(int id, string endpoint)
    {
        var locationId = id.ToString();
        if (!string.IsNullOrEmpty(locationId)) {
            return await GetLocations(endpoint + locationId);
        }
        return null;
    }
   
    public async Task<List<Location>> GetAllLocations() {
        return await _http.GetFromJsonAsync<List<Location>>($"api/locations");
    }

    public async Task<Location> GetLocation(int id)
    {
        return await _http.GetFromJsonAsync<Location>($"api/location/{id}");
    }

    public async Task AddLocation(LocationDTO locationDTO)
    {
        Location location = new Location{
            Name = locationDTO.Name,
            LocationTypeId = locationDTO.LocationTypeId,
            ParentId = locationDTO.ParentId
        };

        await _http.PostAsJsonAsync("api/location", location);
    }

    public async Task EditLocation(Location location)
    {
        await _http.PutAsJsonAsync("api/location", location);
    }
    
    public async Task DeleteLocation(int id)
    {
        await _http.DeleteAsync($"api/location/{id}");
    }


    // Location types: used to register new locations
    public async Task<List<LocationType>> GetLocationTypes() {
        return await _http.GetFromJsonAsync<List<LocationType>>($"api/location-types");
    }

    public async Task<List<Location>> GetLocationsByType(int id) {
        return await _http.GetFromJsonAsync<List<Location>>($"api/locations/location-type/{id}");
    }

}
