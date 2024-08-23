using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AbronalFreelance.Server.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class PaymentTypeController : ControllerBase
{
    private readonly AppDbContext _db;
    public PaymentTypeController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("payment-types")]
    public async Task<IActionResult> GetAllPaymentTypes() {
        return Ok(await _db.PaymentTypes.ToListAsync());
    }
}
