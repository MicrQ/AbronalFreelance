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
        if (user == null) return NotFound("User Not Found!");

        List<FreelancerSkill> freelancerSkills = _db.FreelancerSkills.Where(fs => fs.UserId == UserId).ToList();
        List<FreelancerField> freelancerFields= _db.FreelancerFields.Where(ff => ff.UserId == UserId).ToList();

        ProfileDTO profileDTO = new ProfileDTO() {
            FullName = user.FirstName + " " + user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            Phone = user.PhoneNumber,
            TopSkills = freelancerSkills != null ? freelancerSkills : new List<FreelancerSkill>(),
            TopFields = freelancerFields != null ? freelancerFields : new List<FreelancerField>(),
            Location = string.Join(", ", GetUserLocation(user.LocationId))
        };
    }

    private List<string> GetUserLocation(int LocationId) {
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

                return location;
            }
        }

        return location;
    }
    
}
