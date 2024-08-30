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

    [HttpGet("{userId}/applications")]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> GetAllApplications(string userId) {
        // GET /api/{userId}/applications
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return NotFound(
            new List<ApplicationDTO> {
                new ApplicationDTO {
                    Message = "User Not Found"
                }
            });

        var applications = await _db.Applications
            .Where(a => a.FreelancerId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

        List<ApplicationDTO> applicationDTOs = new List<ApplicationDTO>();
            
        foreach (var app in applications) {
            applicationDTOs.Add(new ApplicationDTO {
                Id = app.Id,
                FreelancerId = app.FreelancerId,
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


    [HttpGet("job/{jobId}/applications")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetAllApplicationsByJobId(string userId, int jobId) {
        // GET api/job/{jobId}?userid={userid}
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
            applicationDTOs.Add(new ApplicationDTO {
                Id = app.Id,
                FreelancerId = app.FreelancerId,
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