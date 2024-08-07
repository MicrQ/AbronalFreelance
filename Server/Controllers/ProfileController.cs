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
        // GET /api/user/profile?UserId={id}
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        if (user == null) return NotFound(
            new ProfileDTO { Flag = false, Message = "User not found!" }
        );

        var profile = await _db.Profiles.FirstOrDefaultAsync(u => u.UserId == UserId);

        List<FreelancerSkill> freelancerSkills = _db.FreelancerSkills.Where(fs => fs.UserId == UserId).ToList();
        List<FreelancerField> freelancerFields= _db.FreelancerFields.Where(ff => ff.UserId == UserId).ToList();

        ProfileDTO profileDTO = new ProfileDTO() {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Headline = profile != null ? profile.Headline : null,
            FreelancerFields = GetFreelancerFieldString(freelancerFields),
            Email = user.Email,
            Phone = user.PhoneNumber,
            TopSkills = freelancerSkills != null ? freelancerSkills : new List<FreelancerSkill>(),
            TopFields = freelancerFields != null ? freelancerFields : new List<FreelancerField>(),
            Location = string.Join(", ", GetUserLocation(user.LocationId)),
            LocationId = user.LocationId,
            Flag = true,
            Message = "Request Succeed."
        };

        return Ok(profileDTO);
    }

    private List<string> GetUserLocation(int LocationId) {
        // used to generate the string version of the user address
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
            }
        }

        return location;
    }
    private string GetFreelancerFieldString(List<FreelancerField> ff) {
        if (ff == null || ff.Count == 0) return "";

        var field = _db.Fields.ToList();
        List<string> fields = new List<string>();

        foreach (FreelancerField f in ff) {
            fields.Add(field.FirstOrDefault(fld => fld.Id == f.FieldId).Name);
        }

        return string.Join(", ", fields);
    }


    [HttpPut("user/profile")]
    public async Task<IActionResult> UpdateProfile(ProfileDTO profileDTO, string UserId) {
        // PUT /api/user/profile?userid={id}
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        if (user == null) return NotFound(new { Message = "User Not Found" });

        user.FirstName = profileDTO.FirstName;
        user.LastName = profileDTO.LastName;
        user.LocationId = profileDTO.LocationId;

        _db.Users.Update(user);

        var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.UserId == UserId);
        bool isNew = false;
        if (profile == null) {
            isNew = true;
            profile = new Profile() { UserId = UserId };
        }
        
        profile.Headline = profileDTO.Headline;

        if (isNew)
            _db.Profiles.Add(profile);
        else
            _db.Profiles.Update(profile);

        var fields = _db.FreelancerFields.Where(ff => ff.UserId == UserId).ToList();
        foreach (var field in fields) {
            _db.FreelancerFields.Remove(field);
        }

        await _db.SaveChangesAsync();
        foreach (var fld in profileDTO.TopFields) {
            // delete all data of user before any update here
            await _db.FreelancerFields.AddAsync(fld);
        }

        // foreach (var skill in profileDTO.TopSkills) {
        //     _db.FreelancerSkills.Add(skill);
        // }

        await _db.SaveChangesAsync();

        return Ok(new { Message = "Profile Updated Successfully." });
    }
    
}
