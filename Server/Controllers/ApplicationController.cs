using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using AbronalFreelance.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Server.Controllers;

[Route("api")]
[ApiController]
[Authorize]
public class ApplicationController : ControllerBase
{
    private readonly AppDbContext _db;

    public ApplicationController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("users/{userId}/applications")]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> GetAllApplications(string userId) {
        // GET /api/users/{userId}/applications
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return NotFound(
            new List<ApplicationDTO> {
                new ApplicationDTO {
                    Message = "User Not Found"
                }
            });

        var applications = await _db.Applications.
            Include(a => a.Job)
            .Where(a => a.FreelancerId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

        List<ApplicationDTO> applicationDTOs = new List<ApplicationDTO>();
            
        foreach (var app in applications) {
            applicationDTOs.Add(new ApplicationDTO {
                Id = app.Id,
                FreelancerId = app.FreelancerId,
                JobId = app.JobId,
                JobTitle = app.Job.Title,
                JobBudget = app.Job.Budget,
                Proposal = app.Proposal,
                DeliveryTime = app.DeliveryTime,
                Amount = app.Amount,
                CreatedAt = app.CreatedAt,
                Flag = true
            });
        }
        return Ok(applicationDTOs);
    }


    [HttpGet("job/{jobId}/applications")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetAllApplicationsByJobId(string userId, int jobId) {
        // GET api/job/{jobId}/applications?userid={userid}
        var job = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == jobId);
        if (job == null) return NotFound(
            new List<ApplicationDTO> {
                new ApplicationDTO {
                    Message = "User Not Found"
                }
            }
        );

        if (job.UserId != userId) return BadRequest(
            new List<ApplicationDTO> {
                new ApplicationDTO {
                    Message = "You don't have permission to view this job"
                }
            }
        );

        var applications = await _db.Applications
            .Where(a => a.JobId == jobId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
        
        List<ApplicationDTO> applicationDTOs = new List<ApplicationDTO>();
            
        foreach (var app in applications) {
            var applicant = await _db.Users.FirstOrDefaultAsync(u => u.Id == app.FreelancerId);
            applicationDTOs.Add(new ApplicationDTO {
                Id = app.Id,
                FreelancerId = app.FreelancerId,
                FreelancerFullName = applicant.FirstName + " " + applicant.LastName,
                JobId = app.JobId,
                Proposal = app.Proposal,
                DeliveryTime = app.DeliveryTime,
                Amount = app.Amount,
                CreatedAt = app.CreatedAt,
                Flag = true
            });
        }
        return Ok(applicationDTOs);
    }


    [HttpGet("application/{appId}")]
    public async Task<IActionResult> GetApplication([FromQuery] string userId, int appId) {
        // GET /api/application/{appid}?userid={userid}
        var application = await _db.Applications
                                .Include(a => a.Job)
                                .Include(a => a.Freelancer)
                                .FirstOrDefaultAsync(a => a.Id == appId);
        if (application == null) 
            return NotFound(new ApplicationDTO { Message = "Application Not Found" });

        if (application.FreelancerId != userId && 
            (application.Job == null || application.Job.UserId != userId)) {
            return BadRequest(new ApplicationDTO { Message = "You don't have permission to view this application" });
        }

        return Ok(new ApplicationDTO {
            Id = application.Id,
            FreelancerId = application.FreelancerId,
            FreelancerFullName = application.Freelancer.FirstName + " " + application.Freelancer.LastName,
            JobId = application.JobId,
            Proposal = application.Proposal,
            DeliveryTime = application.DeliveryTime,
            Amount = application.Amount,
            CreatedAt = application.CreatedAt,
            Flag = true
        });
    }



    [HttpPost("application")]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> CreateApplication(ApplicationDTO applicationDTO) {
        // POST /api/application
        if (await _db.Users.FirstOrDefaultAsync(
            u => u.Id == applicationDTO.FreelancerId) == null)
            return BadRequest("Invalid User Id");

        var job = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == applicationDTO.JobId);
        if (job == null) return BadRequest("Invalid Job Id");

        var applicationExists = await _db.Applications.FirstOrDefaultAsync(
            a => a.FreelancerId == applicationDTO.FreelancerId && a.JobId == applicationDTO.JobId
        );

        if (applicationExists != null) return BadRequest(new ApplicationDTO {
            Message = "Application-Already-Exists"
        });

        Application application = new Application {
            FreelancerId = applicationDTO.FreelancerId,
            JobId = (int)applicationDTO.JobId,
            Proposal = applicationDTO.Proposal,
            DeliveryTime = applicationDTO.DeliveryTime,
            Amount = (double)applicationDTO.Amount
        };

        _db.Applications.Add(application);
        await _db.SaveChangesAsync();

        return Ok(new ApplicationDTO { Flag = true, Message = "Application-Submitted-Successfully." });
    }
}