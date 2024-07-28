using AbronalFreelance.Server.Data;
using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [Authorize(Roles = "Client")]
    [HttpPost("company")]
    public async Task<IActionResult> AddCompany(CompanyInfoDTO companyInfo) {
        // used to add user's company info
        var company = new Clientt {
            UserId = companyInfo.UserId,
            CompanyName = companyInfo.CompanyName,
            CompanyLocationId = companyInfo.CompanyLocationId,
            TinNo = companyInfo.TinNo,
            CompanyEstablishedAt = companyInfo.CompanyStablishedAt
        };

        await _db.Clients.AddAsync(company);
        await _db.SaveChangesAsync();

        return Ok();
    }
}
