using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Server.Controllers
{
    [Route("api/company")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class CompanyController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CompanyController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        // GET api/company?userid={userId}
        public async Task<IActionResult> GetCompany(string userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound(
                new CompanyDTO{ Message = "User not found", Flag = false }
            );

            var company = await _db.Clients.FirstOrDefaultAsync(c => c.UserId == userId);

            return Ok(company == null ? new CompanyDTO {
                    Message = "No Company Information",
                    Flag = false
                } : new CompanyDTO {
                    UserId = company.UserId,
                    Name = company.CompanyName,
                    TinNo = company.TinNo,
                    LocationId = company.CompanyLocationId,
                    LocationString = string.Join(", ", GetLocationString(company.CompanyLocationId)),
                    EstablishedDate = company.CompanyEstablishedAt,
                    Flag = true
                });
        }

        [HttpPut]
        // PUT api/company?userid={userId}
        public async Task<IActionResult> UpdateCompany(CompanyDTO companyDTO, string userId) {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound(
                new CompanyDTO{ Message = "User not found", Flag = false }
            );

            var company = await _db.Clients.FirstOrDefaultAsync(c => c.UserId == userId);
            if (company == null) {
                company = new Clientt {
                    UserId = userId,
                    CompanyName = companyDTO.Name,
                    TinNo = companyDTO.TinNo,
                    CompanyLocationId = companyDTO.LocationId,
                    CompanyEstablishedAt = companyDTO.EstablishedDate
                };
                await _db.Clients.AddAsync(company);
            } else {
                company.CompanyName = companyDTO.Name;
                company.TinNo = companyDTO.TinNo;
                company.CompanyLocationId = companyDTO.LocationId;
                company.CompanyEstablishedAt = companyDTO.EstablishedDate;
            }

            await _db.SaveChangesAsync();

            return Ok(new CompanyDTO {
                Message = "Company Information Updated",
                Flag = true
            });
        }


        private List<string> GetLocationString(int? LocationId) {
        // used to generate the string version of the givel address id
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
    }
}
