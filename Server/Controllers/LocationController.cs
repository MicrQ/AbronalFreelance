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


    // GET /api/locations
    [HttpGet("locations")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLocations() {
        var locations = await _db.Locations.ToListAsync();
        return Ok(locations);
    }

    // GET /api/location/{id}
    [HttpGet("location/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLocation(int id) {
        var location = await _db.Locations.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(location);
    }

    // POST /api/location
    [HttpPost("location")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateLocation(Location location) {
        await _db.Locations.AddAsync(location);
        await _db.SaveChangesAsync();
        return Ok(location);
    }

    // PUT /api/location
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

    // DELETE /api/location/{id}
    [HttpDelete("location/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteLocation(int id) {
        var loc = await _db.Locations.FirstOrDefaultAsync(x => x.Id == id);
        if (loc == null) return NotFound();

        _db.Locations.Remove(loc);
        await _db.SaveChangesAsync();
        return Ok();
    }

    // GET /api/location-types
    [HttpGet("location-types")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLocationTypes() {
        return Ok(await _db.LocationTypes.ToListAsync());
    }

    // GET /api/locations/location-type/{id}
    [HttpGet("locations/location-type/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLocationsByType(int id) {
        return Ok(await _db.Locations.Where(loc => loc.LocationTypeId == id).ToListAsync());
    }





    // GET /api/location/countries
    [HttpGet("location/countries")]
    public async Task<IActionResult> GetCountries() {
        return Ok(await _db.Locations.Where(x => x.ParentId == null).ToListAsync());
    }

    // GET /api/location/regions/{countryId}
    [HttpGet("location/regions/{countryId}")]
    public async Task<IActionResult> GetRegions(int countryId) {
        return Ok(await _db.Locations.Where(x => x.ParentId == countryId).ToListAsync());
    }

    // GET /api/location/cities/{regionId}
    // cities of given region
    [HttpGet("location/cities/{regionId}")]
    public async Task<IActionResult> GetCities(int regionId) {
        return Ok(await _db.Locations.Where(x => x.ParentId == regionId).ToListAsync());
    }

    // GET all cities only
    [HttpGet("location/cities")]
    public async Task<IActionResult> GetAllCities() {
        return Ok(_db.Locations.Include(l => l.LocationTypeId).Where(l => l.LocationType.Name == "CTY"));
    }
}
