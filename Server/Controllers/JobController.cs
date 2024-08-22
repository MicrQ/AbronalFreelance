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

    [HttpGet("job/{id}")]
    public async Task<IActionResult> GetJob(int id) {
        // GET api/job/{id}
        var job = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == id);

        if (job == null) return NotFound(new JobDTO {
            Message = "Job with the given id is not found"
        });

        return Ok(new JobDTO {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            Budget = job.Budget,
            Duration = job.Duration,
            UserId = job.UserId,
            Deadline = job.Deadline,
            LocationId = job.LocationId,
            PaymentTypeId = job.PaymentTypeId,
            JobTypeId = job.JobTypeId,
            CreatedAt = job.CreatedAt,
            Flag = true
        });
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