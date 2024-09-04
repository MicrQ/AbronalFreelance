using AbronalFreelance.Server.Data;
using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbronalFreelance.Server.Controllers;

[ApiController]
[Route("api")]
public class FreelancerController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly RoleManager<IdentityRole> _roleManager;
    public FreelancerController(AppDbContext db, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _roleManager = roleManager;
    }

    [HttpGet("top-freelancers")]
    public async Task<IActionResult> GetTopFreelancers()
    {
        var freelancerRoleId = await _roleManager.Roles
            .Where(r => r.Name == "Freelancer")
            .Select(r => r.Id)
            .FirstOrDefaultAsync();


        List<Profile> topFreelancers = await _db.Profiles
            .Include(p => p.User)
            .Where(p => _db.UserRoles.Any(ur => ur.UserId == p.User.Id && ur.RoleId == freelancerRoleId))
            .OrderByDescending(p => p.AverageRating)
            .Take(10)
            .ToListAsync();
        
        List<FreelancerProfileDTO> topFreelancersDTO = new List<FreelancerProfileDTO>();

        foreach (var profile in topFreelancers) {
            topFreelancersDTO.Add(new FreelancerProfileDTO {
                UserId = profile.UserId,
                AverageRating = profile.AverageRating,
                FirstName = profile.User.FirstName,
                LastName = profile.User.LastName,
                Headline = profile.Headline,
                Email = profile.User.Email,
                Phone = profile.User.PhoneNumber,
                CreatedAt = profile.CreatedAt,
                LocationId = profile.User.LocationId,
                Flag = true
            });
        }

        return Ok(topFreelancersDTO);
    }

    [HttpPost("top-freelancers/filter")]
    public async Task<IActionResult> GetFreelancersByFilter(int? locationId, int? categoryId, double? rating)
    {
        var freelancerRoleId = await _roleManager.Roles
            .Where(r => r.Name == "Freelancer")
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        List<Profile> freelancers = await _db.Profiles
            .Include(p => p.User)
            .Where(p => _db.UserRoles.Any(ur => ur.UserId == p.User.Id && ur.RoleId == freelancerRoleId))
            .ToListAsync();

        if (locationId != null)
        {
            freelancers = freelancers.Where(f => f.User.LocationId == locationId).ToList();
        }

        // if (categoryId != null)
        // {
        //     freelancers = freelancers.Where(f => f.User.CategoryId == categoryId).ToList();
        // }

        if (rating != null)
        {
            freelancers = freelancers.Where(f => f.AverageRating >= rating).ToList();
        }

        List<FreelancerProfileDTO> freelancersDTO = new List<FreelancerProfileDTO>();

        foreach (var profile in freelancers)
        {
            freelancersDTO.Add(new FreelancerProfileDTO
            {
                UserId = profile.UserId,
                AverageRating = profile.AverageRating,
                FirstName = profile.User.FirstName,
                LastName = profile.User.LastName,
                Headline = profile.Headline,
                Email = profile.User.Email,
                Phone = profile.User.PhoneNumber,
                CreatedAt = profile.CreatedAt,
                LocationId = profile.User.LocationId,
                Flag = true
            });
        }

        return Ok(freelancersDTO);
    }
}