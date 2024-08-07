using AbronalFreelance.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbronalFreelance.Server.Controllers;

[ApiController]
[Route("api")]
public class SkillController : ControllerBase
{
    private readonly AppDbContext _db;
    public SkillController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("skills")]
    public async Task<IActionResult> GetAllSkills() {
        // GET /api/skills
        return Ok(await _db.Skills.ToListAsync());
    }
}
