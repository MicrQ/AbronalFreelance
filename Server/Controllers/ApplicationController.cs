using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using AbronalFreelance.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Server.Controllers;

[Route("api")]
[ApiController]
[Authorize(Roles = "Freelancer")]
public class ApplicationController : ControllerBase
{
    private readonly AppDbContext _db;

    public ApplicationController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("application/{id}")]
    public IActionResult GetApplication(int id) {
        // GET /api/application/{id}
        var application = _db.Applications.FirstOrDefault(x => x.Id == id);
        if (application == null) return NotFound(new ApplicationDTO { Message = "Application Not Found" });

        return Ok(new ApplicationDTO {
            Id = application.Id,
            FreelancerId = application.FreelancerId,
            JobId = application.JobId,
            Proposal = application.Proposal,
            DeliveryTime = application.DeliveryTime,
            Amount = application.Amount,
            CreatedAt = application.CreatedAt,
            Flag = true
        });
    }

    [HttpPost("application")]
    public async Task<IActionResult> CreateApplication(ApplicationDTO applicationDTO) {
        // POST /api/application
        if (await _db.Users.FirstOrDefaultAsync(
            u => u.Id == applicationDTO.FreelancerId) == null)
            return BadRequest("Invalid User Id");

        var job = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == applicationDTO.JobId);
        if (job == null) return BadRequest("Invalid Job Id");

        Application application = new Application {
            FreelancerId = applicationDTO.FreelancerId,
            JobId = (int)applicationDTO.JobId,
            Proposal = applicationDTO.Proposal,
            DeliveryTime = applicationDTO.DeliveryTime,
            Amount = (double)applicationDTO.Amount
        };

        _db.Applications.Add(application);
        await _db.SaveChangesAsync();

        return Ok(new ApplicationDTO { Flag = true, Message = "Application Created" });
    }
}