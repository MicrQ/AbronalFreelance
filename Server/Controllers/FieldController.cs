using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace AbronalFreelance.Server;

[ApiController]
[Route("api")]
public class FieldController : ControllerBase
{
    private readonly AppDbContext _db;
    public FieldController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("fields")]
    public async Task<IActionResult> GetAllFields() {
        // GET /api/fields
        return Ok(await _db.Fields.ToListAsync());
    }

}
