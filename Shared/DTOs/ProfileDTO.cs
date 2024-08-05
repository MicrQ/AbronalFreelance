using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Shared;

public class ProfileDTO
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string? Headline { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public List<FreelancerSkill?>? TopSkills { get; set; }
    // public List<Field>? TopFields { get; set; }
}


public async Task<IActionResult> GetUserProfiles(string UserId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (user == null) return NotFound("User not found.");

            var profile = await _db.Profiles.FirstOrDefaultAsync(u => u.UserId == UserId);
            if (profile == null) return NotFound("Profile not found.");

            var locationList = GetUserLocation(user.LocationId);
            if (locationList == null) return NotFound("User location not found.");

            var userSkills = await _db.FreelancerSkills.Where(s => s.UserId == UserId).ToListAsync();
            if (userSkills == null) userSkills = new List<FreelancerSkill>();

            var userProfile = new ProfileDTO()
            {
                FullName = user.FirstName + " " + user.LastName,
                UserName = user.UserName,
                Headline = profile.Headline,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Location = string.Join(",", locationList),
                TopSkills = userSkills
            };

            return Ok(userProfile);
        }

        private List<string> GetUserLocation(int LocationId)
        {
            int? loc_id = LocationId;
            List<string> location = new List<string>();
            List<Location> locations = _db.Locations.ToList();

            while (loc_id != null)
            {
                var loc = locations.FirstOrDefault(l => l.Id == loc_id);
                if (loc == null) break;
                location.Add(loc.Name);
                loc_id = loc.ParentId;
            }

            return location;
        }