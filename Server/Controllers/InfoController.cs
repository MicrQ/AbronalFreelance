using AbronalFreelance.Server.Data;
using AbronalFreelance.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbronalFreelance.Server.Controllers;

[ApiController]
[Route("api")]
public class InfoController : ControllerBase
{
    private readonly AppDbContext _db;
    public InfoController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("client/{userId}/info")]
    public async Task<IActionResult> GetClientInfo(string userId) {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return NotFound(new InfoDTO { Flag = false, Message = "User Not Found" });

        var numberOfJobs = await _db.Jobs.Where(j => j.UserId == userId).CountAsync();
        return Ok(new InfoDTO { Flag = true, Message = "Info Retrieved Successfully", Jobs = numberOfJobs });
    }
}