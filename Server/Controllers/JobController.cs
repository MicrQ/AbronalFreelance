using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using Microsoft.AspNetCore.Authorization;
using AbronalFreelacner.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Server.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class JobController : ControllerBase
{
    private readonly AppDbContext _db;

    public JobController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("jobs")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddJob(JobDTO jobDTO) {
        // POST api/jobs
        if (await _db.Users.FirstOrDefaultAsync(u => u.Id == jobDTO.UserId) == null) {
            return NotFound(new JobDTO { Message = "User not found"});
        }

        Job job = new Job {
            Title = jobDTO.Title,
            Description = jobDTO.Description,
            Budget = jobDTO.Budget,
            Duration = jobDTO.Duration,
            UserId = jobDTO.UserId,
            Deadline = (DateTime)jobDTO.Deadline,
            LocationId = (int)jobDTO.LocationId,
            PaymentTypeId = (int)jobDTO.PaymentTypeId,
            JobTypeId = (int)jobDTO.JobTypeId
        };

        _db.Jobs.Add(job);
        await _db.SaveChangesAsync();

        return Created("uri of the created job...", new JobDTO {
            Flag = true,
            Message = "Job Posted Successfully."
        });
    }
}