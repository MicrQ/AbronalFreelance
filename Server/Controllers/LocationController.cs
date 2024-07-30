using AbronalFreelance.Server.Data;
using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AbronalFreelance.Server.Controllers;

[Route("api")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly AppDbContext _db;

    public LocationController(AppDbContext db)
    {
        _db = db;
    }


    //get All
    [HttpGet("locations")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLocations() {
        var locations = await _db.Locations.ToListAsync();
        return Ok(locations);
    }

    //get One loc
    [HttpGet("location/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLocation(int id) {
        var location = await _db.Locations.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(location);
    }

    // add One loc
    [HttpPost("location")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateLocation(Location location) {
        await _db.Locations.AddAsync(location);
        await _db.SaveChangesAsync();
        return Ok(location);
    }

    // update loc
    [HttpPut("location")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateLocation(Location location) {
        var loc = await _db.Locations.FirstOrDefaultAsync(x => x.Id == location.Id);
        if (loc == null) return NotFound();

        _db.Entry(loc).CurrentValues.SetValues(location);
        _db.Entry(loc).State = EntityState.Modified;

        await _db.SaveChangesAsync();
        return Ok(loc);
    }

    // Delete Loc
    [HttpDelete("location/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteLocation(int id) {
        var loc = await _db.Locations.FirstOrDefaultAsync(x => x.Id == id);
        if (loc == null) return NotFound();

        _db.Locations.Remove(loc);
        await _db.SaveChangesAsync();
        return Ok();
    }

    // get location types
    [HttpGet("location-types")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLocationTypes() {
        return Ok(await _db.LocationTypes.ToListAsync());
    }

    [HttpGet("locations/location-type/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLocationsByType(int id) {
        return Ok(await _db.Locations.Where(loc => loc.LocationTypeId == id).ToListAsync());
    }





    // location data for registration
    // countries
    [HttpGet("location/countries")]
    public async Task<IActionResult> GetCountries() {
        return Ok(await _db.Locations.Where(x => x.ParentId == null).ToListAsync());
    }

    // regions
    [HttpGet("location/regions/{countryId}")]
    public async Task<IActionResult> GetRegions(int countryId) {
        return Ok(await _db.Locations.Where(x => x.ParentId == countryId).ToListAsync());
    }

    //cities
    [HttpGet("location/cities/{regionId}")]
    public async Task<IActionResult> GetCities(int regionId) {
        return Ok(await _db.Locations.Where(x => x.ParentId == regionId).ToListAsync());
    }
}
