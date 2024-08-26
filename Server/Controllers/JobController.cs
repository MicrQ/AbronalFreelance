using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using Microsoft.AspNetCore.Authorization;
using AbronalFreelance.Shared.DTOs;
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

    [HttpGet("{userId}/jobs")]
    public async Task<IActionResult> GetAllJobs(string userId) {
        // GET api/jobs?userid={userid}
        var jobs = await _db.Jobs.Where(j => j.UserId == userId).ToListAsync();
        if (jobs == null) return NotFound(new JobDTO { Message = "No jobs found" });

        List<JobDTO> userJobs = new List<JobDTO>();
        foreach (Job job in jobs) {
            var skills = await _db.SkillsForJobs.Where(j => j.JobId == job.Id).ToListAsync();
            var fields = await _db.JobFields.Where(j => j.JobId == job.Id).ToListAsync();
            userJobs.Add(new JobDTO {
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
                Skills = skills != null ? skills : new List<SkillsForJob>(),
                Fields = fields != null ? fields : new List<JobFields>()
            });
        }
        return Ok(userJobs);
    }

    [HttpGet("job/{id}")]
    public async Task<IActionResult> GetJob(int id) {
        // GET api/job/{id}
        var job = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == id);

        if (job == null) return NotFound(new JobDTO {
            Message = "Job with the given id is not found"
        });
        var skills = await _db.SkillsForJobs.Where(j => j.JobId == id).ToListAsync();
        var fields = await _db.JobFields.Where(j => j.JobId == id).ToListAsync();

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
            Skills = skills != null ? skills : new List<SkillsForJob>(),
            Fields = fields != null ? fields : new List<JobFields>(),
            Flag = true
        });
    }



    [HttpPost("jobs")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddJob(JobDTO jobDTO) {
        // POST api/jobs
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == jobDTO.UserId);
        if (user == null) {
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

        foreach (var skill in jobDTO.Skills) {
            _db.SkillsForJobs.Add(skill);
        }
        foreach (var field in jobDTO.Fields) {
            _db.JobFields.Add(field);
        }
        await _db.SaveChangesAsync();

        return Created("uri of the created job...", new JobDTO {
            Flag = true,
            Message = "Job Posted Successfully."
        });
    }


    [HttpPut("job/{id}")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdateJob(int id, string UserId, JobDTO jobDTO) {
        // PUT api/job/{id}?userid={userid}
        if (UserId != jobDTO.UserId) {
            return BadRequest(new JobDTO {
                Message = "You don't have the permission to update this job"
            });
        }

        var job = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == id && j.UserId == UserId);
        if (job == null) return NotFound(new JobDTO {
            Message = "Job with the given id is not found"
        });

        job.Title = jobDTO.Title;
        job.Description = jobDTO.Description;
        job.Budget = jobDTO.Budget;
        job.Duration = jobDTO.Duration;
        job.Deadline = (DateTime)jobDTO.Deadline;
        job.LocationId = (int)jobDTO.LocationId;
        job.PaymentTypeId = (int)jobDTO.PaymentTypeId;
        job.JobTypeId = (int)jobDTO.JobTypeId;
        _db.Jobs.Update(job);

        var skills = await _db.SkillsForJobs.Where(j => j.Id == id).ToListAsync();
        var fields = await _db.JobFields.Where(j => j.Id == id).ToListAsync();

        foreach (var skill in skills) {
            _db.SkillsForJobs.Remove(skill);
        }
        foreach (var field in fields) {
            _db.JobFields.Remove(field);
        }

        foreach (var skill in jobDTO.Skills) {
            _db.SkillsForJobs.Add(skill);
        }
        foreach (var field in jobDTO.Fields) {
            _db.JobFields.Add(field);
        }

        await _db.SaveChangesAsync();

        return Ok(new JobDTO {
            Flag = true,
            Message = "Job Updated Successfully."
        });
    }
}