using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AbronalFreelance.Server.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class JobTypeController : ControllerBase
{
    private readonly AppDbContext _db;
    public JobTypeController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("job-types")]
    public async Task<IActionResult> GetAllJobTypes() {
        return Ok(await _db.JobTypes.ToListAsync());
    }
}
