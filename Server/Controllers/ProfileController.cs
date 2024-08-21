using AbronalFreelance.Server.Data;
using AbronalFreelance.Shared;
using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbronalFreelance.Server.Controllers;

[Route("api")]
[Authorize]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;

    public ProfileController(AppDbContext db, UserManager<User> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet("freelancer/profile")]
    public async Task<IActionResult> GetFreelancerProfiles(string UserId) {
        // GET /api/user/profile?UserId={id}
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        if (user == null) return NotFound(
            new FreelancerProfileDTO { Flag = false, Message = "User not found!" }
        );

        var profile = await _db.Profiles.FirstOrDefaultAsync(u => u.UserId == UserId);

        List<FreelancerSkill> freelancerSkills = _db.FreelancerSkills.Where(fs => fs.UserId == UserId).ToList();
        List<FreelancerField> freelancerFields= _db.FreelancerFields.Where(ff => ff.UserId == UserId).ToList();

        FreelancerProfileDTO profileDTO = new FreelancerProfileDTO() {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            Phone = user.PhoneNumber,
            Headline = profile != null ? profile.Headline : null,
            FreelancerFields = GetFreelancerFieldString(freelancerFields).Length > 0 ? GetFreelancerFieldString(freelancerFields) + ", ..." : "",
            FreelancerSkills = GetFreelancerSkillString(freelancerSkills).Length > 0 ? GetFreelancerSkillString(freelancerSkills) + ", ..." : "",
            TopSkills = freelancerSkills != null ? freelancerSkills : new List<FreelancerSkill>(),
            TopFields = freelancerFields != null ? freelancerFields : new List<FreelancerField>(),
            Location = string.Join(", ", GetUserLocation(user.LocationId)),
            LocationId = user.LocationId,
            Flag = true,
            Message = "Request Succeed."
        };

        return Ok(profileDTO);
    }


    [HttpPut("freelancer/profile")]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> UpdateFreelancerProfile(FreelancerProfileDTO profileDTO, string UserId)
    {
        // PUT /api/user/profile?userid={id}
        // var user = await _db.Users
        //     .FirstOrDefaultAsync(u => u.Id == UserId);
        var user = await _userManager.FindByIdAsync(UserId);
        
        if (user == null) 
            return NotFound(new { Message = "User Not Found" });


        // Update user fields...more to be added
        user.FirstName = profileDTO.FirstName;
        user.LastName = profileDTO.LastName;
        user.LocationId = profileDTO.LocationId;
        user.PhoneNumber = profileDTO.Phone;

        // Update or add profile
        var Profile = _db.Profiles.FirstOrDefault(p => p.UserId == user.Id);
        if (Profile == null) 
        {
            Profile profile = new Profile { UserId = UserId, Headline = profileDTO.Headline };
            _db.Profiles.Add(profile);
        } 
        else 
        {
            Profile.Headline = profileDTO.Headline;
            _db.Profiles.Update(Profile);
        }

        // Remove old FreelancerFields and add new ones
        var FreelancerFields = _db.FreelancerFields.Where(ff => ff.UserId == UserId);
        if (FreelancerFields != null) {
            foreach (var freefield in FreelancerFields) {
                _db.FreelancerFields.Remove(freefield);
            }
            await _db.SaveChangesAsync();
        }

        foreach (var fld in profileDTO.TopFields) 
        {
            _db.FreelancerFields.Add(fld);
        }

        // Remove old FreelancerSkills and add new ones
        var FreelancerSkills = _db.FreelancerSkills.Where(ss => ss.UserId == UserId);
        if (FreelancerSkills != null) {
            foreach (var freeskill in FreelancerSkills) {
                _db.FreelancerSkills.Remove(freeskill);
            }
            await _db.SaveChangesAsync();
        }

        foreach (var skl in profileDTO.TopSkills) 
        {
            _db.FreelancerSkills.Add(skl);
        }


        await _db.SaveChangesAsync();

        return Ok(new { Message = "Profile Updated Successfully." });
    }


    [HttpGet("client/profile")]
    public async Task<IActionResult> GetClientProfile(string UserId) {
        // GET /api/client/profile?userid=clinetid
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        if (user == null) return NotFound(new {
            Flag = false,
            Message = "User doesn't exsist"
        });
        var company = await _db.Clients.FirstOrDefaultAsync(c => c.UserId == UserId);
        var userProfile = await _db.Profiles.FirstOrDefaultAsync(p => p.UserId == UserId);

        var profile = new ClientProfileDTO {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Headline = userProfile != null ? userProfile.Headline : null,
            Email = user.Email,
            Phone = user.PhoneNumber,
            LocationId = user.LocationId,
            UserId = user.Id
        };
        if (company != null) {
            profile.hasCompany = true;
            profile.CompanyName = company.CompanyName;
            profile.TinNo = company.TinNo;
            profile.CompanyLocationId = company.CompanyLocationId;
            profile.EstablishedDate = company.CompanyEstablishedAt;
        }

        return Ok(profile);
    }

    [HttpPut("client/profile")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdateClientProfile(ClientProfileDTO cpDTO) {
        // PUT /api/client/profile?userid={userid}
        var user = await _db.Users.FirstOrDefaultAsync(
            u => u.Id == cpDTO.UserId
        );
        if (user == null) return NotFound(new { Flag = false, Message = "User Not Found" });

        var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.UserId == cpDTO.UserId);

        user.FirstName = cpDTO.FirstName;
        user.LastName = cpDTO.LastName;
        // user.UserName = cpDTO.UserName;
        // user.Email = cpDTO.Email;
        user.PhoneNumber = cpDTO.Phone;
        user.LocationId = cpDTO.LocationId;
        if (profile == null) {
            _db.Profiles.Add(new Profile {
                UserId = cpDTO.UserId,
                Headline = cpDTO.Headline
            });
        } else {
            profile.Headline = cpDTO.Headline;
            _db.Profiles.Update(profile);
        }

        if (cpDTO.hasCompany) {
            var company = await _db.Clients.FirstOrDefaultAsync(c => c.UserId == cpDTO.UserId);
            if (company == null) {
                _db.Clients.Add(new Clientt {
                    UserId = cpDTO.UserId,
                    CompanyName = cpDTO.CompanyName,
                    TinNo = cpDTO.TinNo,
                    CompanyLocationId = (int)cpDTO.CompanyLocationId,
                    CompanyEstablishedAt = cpDTO.EstablishedDate
                });
            } else {
                company.CompanyName = cpDTO.CompanyName;
                company.TinNo = cpDTO.TinNo;
                company.CompanyLocationId = (int)cpDTO.CompanyLocationId;
                company.CompanyEstablishedAt = cpDTO.EstablishedDate;
                _db.Clients.Update(company);
            }
        }
        await _db.SaveChangesAsync();

        return Ok(new { Flag = true, Message = "Profile Updated Successfully."});
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
        int count = 0;

        foreach (FreelancerField f in ff) {
            if (count++ == 5) break;
            fields.Add(field.FirstOrDefault(fld => fld.Id == f.FieldId).Name);
        }

        return string.Join(", ", fields);
    }
    private string GetFreelancerSkillString(List<FreelancerSkill>? ss) {
        if (ss == null || ss.Count == 0) return "";

        var skill = _db.Skills.ToList();
        List<string> skills = new List<string>();
        int count = 0;

        foreach (FreelancerSkill s in ss) {
            if (count++ == 5) break;
            skills.Add(skill.FirstOrDefault(skl => skl.Id == s.SkillId).Name);
        }

        return string.Join(", ", skills);
    }
}
