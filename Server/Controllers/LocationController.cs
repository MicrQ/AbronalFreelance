using AbronalFreelance.Server.Data;
using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AbronalFreelance.Server.Controllers;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class LocationController : ControllerBase
{
    private readonly AppDbContext _db;

    public LocationController(AppDbContext db)
    {
        _db = db;
    }


    //get All
    [HttpGet("locations")]
    public async Task<IActionResult> GetLocations() {
        var locations = await _db.Locations.ToListAsync();
        return Ok(locations);
    }

    //get One loc
    [HttpGet("location/{id}")]
    public async Task<IActionResult> GetLocation(int id) {
        var location = await _db.Locations.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(location);
    }

    // add One loc
    [HttpPost("location")]
    public async Task<IActionResult> CreateLocation(Location location) {
        await _db.Locations.AddAsync(location);
        await _db.SaveChangesAsync();
        return Ok(location);
    }

    // update loc
    [HttpPut("location")]
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
    public async Task<IActionResult> DeleteLocation(int id) {
        var loc = await _db.Locations.FirstOrDefaultAsync(x => x.Id == id);
        if (loc == null) return NotFound();

        _db.Locations.Remove(loc);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
