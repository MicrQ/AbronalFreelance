using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Server.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class ContractController : ControllerBase
{
    private readonly AppDbContext _db;
    public ContractController(AppDbContext db) {
        _db = db;
    }

    [HttpPost("contract")]
    public async Task<IActionResult> CreateContract([FromQuery] string userId, ContractDTO contractDTO) {
        // POST api/contract?userid={userid}
        var app = await _db.Applications
            .Include(a => a.Job)
            .FirstOrDefaultAsync(a => a.Id == contractDTO.ApplicationId);
        if (app == null) return NotFound(new ContractDTO { Message = "Application Not Found" });
        if (app.Job.UserId != userId) return BadRequest(
            new ContractDTO { Message = "You don't have permission to create a contract for this application" }
        );

        if (app.Job.IsClosed) return BadRequest(
            new ContractDTO { Message = "You can only create one contract for a Job." }
        );

        _db.Contracts.Add(new Contract {
            ApplicationId = (int)contractDTO.ApplicationId,
            StartDate = (DateTime)contractDTO.StartDate,
            EndDate = (DateTime)contractDTO.EndDate
        });

        var appStatus = await _db.ApplicationStatuses.FirstOrDefaultAsync(a => a.ApplicationId == app.Id);
        if (appStatus == null) {
            _db.ApplicationStatuses.Add(new ApplicationStatus {
                ApplicationId = (int)app.Id,
                ApprovalStatusId = 2
            });
        } else {
            appStatus.ApprovalStatusId = 2;
            _db.ApplicationStatuses.Update(appStatus);
        }

        var job = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == app.JobId);
        job.IsClosed = true;
        _db.Jobs.Update(job);

        await _db.SaveChangesAsync();
        return Ok(new ContractDTO {
            Flag = true,
            Message = "Contract Created Successfully!"
        });
    }

    [HttpGet("user/{userId}/contracts")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetAllContracts(string userId) {
        // GET api/user/{userid}/contracts
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return NotFound(new List<ContractDTO> {
                new ContractDTO { Message = "User Not Found" }
            });

        var contracts = await _db.Contracts
            .Include(c => c.Application)
            .Include(c => c.Application.Job)
            .Where(c => c.Application.Job.UserId == userId)
            .ToListAsync();

        List<ContractDTO> contractDTO = new List<ContractDTO>();
        if (contracts != null) {
            foreach (var contract in contracts) {
                var status = await _db.ContractStatuses
                    .Include(cs => cs.ApprovalStatus)
                    .FirstOrDefaultAsync(cs => cs.ContractId == contract.Id);
                contractDTO.Add(new ContractDTO {
                    Id = contract.Id,
                    ApplicationId = contract.ApplicationId,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    CreatedAt = contract.CreatedAt,
                    UpdatedAt = contract.UpdatedAt,
                    JobTitle = contract.Application.Job.Title,
                    StatusName = status?.ApprovalStatus.Name ?? "Pending",
                    Flag = true
                });
            }
        }

        return Ok(contractDTO);
    }

    [HttpGet("freelancer/{userId}/contracts")]
    public async Task<IActionResult> GetFreelancerContracts(string userId) {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return NotFound(new List<ContractDTO> {
            new ContractDTO { Message = "User Not Found" }
        });

        var contracts = await _db.Contracts
            .Include(c => c.Application)
            .Include(c => c.Application.Job)
            .Where(c => c.Application.FreelancerId == userId)
            .ToListAsync();

        List<ContractDTO> contractDTO = new List<ContractDTO>();
        if (contracts != null) {
            foreach (var contract in contracts) {
                var status = await _db.ContractStatuses
                    .Include(cs => cs.ApprovalStatus)
                    .FirstOrDefaultAsync(cs => cs.ContractId == contract.Id);
                contractDTO.Add(new ContractDTO {
                    Id = contract.Id,
                    ApplicationId = contract.ApplicationId,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    CreatedAt = contract.CreatedAt,
                    UpdatedAt = contract.UpdatedAt,
                    JobTitle = contract.Application.Job.Title,
                    StatusName = status?.ApprovalStatus.Name ?? "Pending",
                    Flag = true
                });
            }
        }
        return Ok(contractDTO);
    }
}
