using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using Microsoft.AspNetCore.Authorization;
using AbronalFreelance.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Server.Controllers;

[ApiController]
[Route("api")]
public class JobController : ControllerBase
{
    private readonly AppDbContext _db;

    public JobController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{userId}/jobs")]
    [Authorize]
    public async Task<IActionResult> GetAllJobs(string userId) {
        // GET api/jobs?userid={userid}
        var jobs = await _db.Jobs.Where(j => j.UserId == userId).ToListAsync();
        if (jobs == null) return NotFound(new JobDTO { Message = "No jobs found" });

        List<JobDTO> userJobs = new List<JobDTO>();
        foreach (Job job in jobs) {
            (List<Skill> skillList, List<Field> fieldList) = await GetSkillsAndFields(job.Id);
            int applications = await _db.Applications.CountAsync(a => a.JobId == job.Id);
            string status = await _db.JobStatuses
                                .Where(js => js.JobId == job.Id)
                                .Join(_db.ApprovalStatuses,
                                      js => js.ApprovalStatusId,
                                      aps => aps.Id,
                                      (js, aps) => aps.Name)
                                .FirstOrDefaultAsync();

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
                Skills = skillList,
                Fields = fieldList,
                Flag = true,
                TotalApplications = applications,
                Status = status
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

        (List<Skill> skills, List<Field> fields) = await GetSkillsAndFields(job.Id);

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
            Skills = skills,
            Fields = fields,
            Flag = true
        });
    }

    [HttpGet("jobs/recent")]
    public async Task<IActionResult> GetRecentJobs(
        int? field = null, int? locationId = null,
        string? title = null, string? userId = null, int limit = 10) {
        // GET api/jobs/recent?userid={userid}&limit={limit}
        List<Job>? jobs;
        if (field.HasValue || locationId.HasValue || !string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(userId)) {
            var query = _db.Jobs.AsQueryable();
            
            if (field.HasValue) {
                query = query.Where(j => _db.JobFields.Any(
                    jf => jf.JobId == j.Id && jf.FieldId == field.Value));
            }
            
            if (locationId.HasValue) {
                query = query.Where(j => j.LocationId == locationId.Value);
            }
            
            if (!string.IsNullOrEmpty(title)) {
                query = query.Where(j => j.Title.Contains(title));
            }
            
            if (!string.IsNullOrEmpty(userId)) {
                query = query.Where(j => j.UserId == userId);
            }
            
            jobs = await query
                .OrderByDescending(j => j.CreatedAt)
                .Take(limit)
                .ToListAsync();
        } else {
            jobs = await _db.Jobs
                .OrderByDescending(j => j.CreatedAt)
                .Take(limit)
                .ToListAsync();
        }

        if (jobs == null) return NotFound(new JobDTO { Message = "No jobs found" });

        List<JobDTO> userJobs = new List<JobDTO>();
        foreach (Job job in jobs) {
            (List<Skill> skillList, List<Field> fieldList) = await GetSkillsAndFields(job.Id);

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
                Skills = skillList,
                Fields = fieldList,
                Flag = true
            });
        }
        return Ok(userJobs);
    }

    [HttpPost("jobs")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddJob(JobDTO jobDTO) {
        // POST api/jobs
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == jobDTO.UserId);
        if (user == null) {
            return NotFound(new JobDTO { Message = "User not found"});
        }
        DateTime filter = DateTime.Now;
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

        var newJob = await _db.Jobs.FirstOrDefaultAsync(j => j.CreatedAt > filter);

        if (newJob != null) {
            foreach (var skill in jobDTO.Skills) {
                _db.SkillsForJobs.Add(new SkillsForJob {
                    JobId = newJob.Id,
                    SkillId = skill.Id
                });
            }
            foreach (var field in jobDTO.Fields) {
                _db.JobFields.Add(new JobFields {
                    JobId = newJob.Id,
                    FieldId = field.Id
                });
            }

            _db.JobStatuses.Add(new JobStatus {
                JobId = newJob.Id,
                ApprovalStatusId = 1,
                DateTime = DateTime.Now
            });
            await _db.SaveChangesAsync();
        }

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

        var skills = await _db.SkillsForJobs.Where(j => j.JobId == id).ToListAsync();
        var fields = await _db.JobFields.Where(j => j.JobId == id).ToListAsync();

        foreach (var skill in skills) {
            _db.SkillsForJobs.Remove(skill);
        }
        foreach (var field in fields) {
            _db.JobFields.Remove(field);
        }
        await _db.SaveChangesAsync();

        foreach (var skill in jobDTO.Skills) {
            _db.SkillsForJobs.Add(new SkillsForJob {
                JobId = job.Id,
                SkillId = skill.Id
            });
        }
        foreach (var field in jobDTO.Fields) {
            _db.JobFields.Add(new JobFields {
                JobId = job.Id,
                FieldId = field.Id
            });
        }

        await _db.SaveChangesAsync();

        return Ok(new JobDTO {
            Flag = true,
            Message = "Job Updated Successfully."
        });
    }

    private async Task<(List<Skill>, List<Field>)> GetSkillsAndFields(int jobId) {
        var skills = await _db.SkillsForJobs.Where(j => j.JobId == jobId).ToListAsync() ?? new List<SkillsForJob>();
        var fields = await _db.JobFields.Where(j => j.JobId == jobId).ToListAsync() ?? new List<JobFields>();

        List<Skill> skillList = new List<Skill>();
        List<Field> fieldList = new List<Field>();
        foreach (var jobSkill in skills) {
            skillList.Add(await _db.Skills.FirstOrDefaultAsync(s => s.Id == jobSkill.SkillId));
        }
        foreach (var jobField in fields) {
            fieldList.Add(await _db.Fields.FirstOrDefaultAsync(f => f.Id == jobField.FieldId));
        }

        return (skillList, fieldList);
    }
}