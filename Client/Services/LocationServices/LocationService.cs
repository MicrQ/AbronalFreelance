using System.Net.Http.Json;
using AbronalFreelance.Client.Pages;
using AbronalFreelance.Client.Services.Auth;
using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Components;


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

}
