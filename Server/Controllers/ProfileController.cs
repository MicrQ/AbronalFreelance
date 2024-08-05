using AbronalFreelance.Server.Data;
using AbronalFreelance.Shared;
using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbronalFreelance.Server.Controllers;

[Route("api")]
[Authorize]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProfileController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("user/profile")]
    [Authorize]
    public async Task<IActionResult> GetUserProfiles(string UserId) {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        if (user == null) return NotFound();
        Profile? profile = await _db.Profiles.FirstOrDefaultAsync(u => u.UserId == UserId);

        var userProfile = new ProfileDTO() {
            FullName = user.FirstName + " " + user.LastName,
            UserName = user.UserName,
            Headline = profile.Headline,
            Email = user.Email,
            Phone = user.PhoneNumber,
            Location = string.Join(",", GetUserLocation(user.LocationId)),
            TopSkills = _db.FreelancerSkills.Where(s => s.UserId == UserId).ToList()!
            // field of user ...
        };

        return Ok(userProfile);
    }

    private List<string?> GetUserLocation(int LocationId) {
        int? loc_id;
        List<string> location = new List<string>();
        List<Location> locations = _db.Locations.ToList();

        foreach (var loc in locations){
            if (loc.Id == LocationId) {
                loc_id = loc.Id;
                while (loc_id != null) {
                    Location Loc = locations.FirstOrDefault(l => l.Id == loc_id);
                    location.Add(Loc.Name);
                    loc_id = Loc.ParentId;
                }

                break;
            }
        }

        return location;
    }
    
}
