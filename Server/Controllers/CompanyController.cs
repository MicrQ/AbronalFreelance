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
                } : company);
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
                    CompanyLocationId = (int)companyDTO.LocationId,
                    CompanyEstablishedAt = (DateTime)companyDTO.EstablishedDate
                };
                await _db.Clients.AddAsync(company);
            } else {
                company.CompanyName = companyDTO.Name;
                company.TinNo = companyDTO.TinNo;
                company.CompanyLocationId = (int)companyDTO.LocationId;
                company.CompanyEstablishedAt = (DateTime)companyDTO.EstablishedDate;
            }

            await _db.SaveChangesAsync();

            return Ok(new CompanyDTO {
                Message = "Company Information Updated",
                Flag = true
            });
        }
    }
}
