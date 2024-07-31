using Microsoft.AspNetCore.Mvc;
using AbronalFreelance.Server.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using AbronalFreelance.Shared.DTOs;
using AbronalFreelance.Shared.Models;
using AbronalFreelance.Shared.ResponseModels;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace AbronalFreelance.Server.Controllers;

[ApiController]
[Route("api/portfolio")]
[Authorize(Roles = "Freelancer")]
public class PortfolioController : ControllerBase
{
    private readonly AppDbContext _db;
    public PortfolioController(AppDbContext db)
    {
        _db = db;
    }


    // Get all portfolios of a user
    [HttpGet]
    public async Task<IActionResult> GetAllPortfolios() {
        return Ok(await _db.FreelancerPortfolios.ToListAsync());
    }


    // Get one portfolio by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPortfolio(int id) {
        var ptf = await _db.FreelancerPortfolios.FirstOrDefaultAsync(p => p.Id == id);
        if (ptf == null) return NotFound();

        return Ok(ptf);
    }


    // add new portfolio
    [HttpPost]
    public async Task<IActionResult> AddPortfolio(PortfolioDTO portfolioDTO) {
        FreelancerPortfolio portfolio = new FreelancerPortfolio {
            UserId = portfolioDTO.UserId,
            Title = portfolioDTO.Title,
            Description = portfolioDTO.Description,
            Link = portfolioDTO.Link
        };

        await _db.FreelancerPortfolios.AddAsync(portfolio);
        await _db.SaveChangesAsync();

        return Ok(new PortfolioResponse {
            Flag = true,
            Message = "Portfolio added."
        });
    }


    // update portfolio
    [HttpPut]
    public async Task<IActionResult> UpdatePortfolio(FreelancerPortfolio portfolio) {
        var ptf = await _db.FreelancerPortfolios.FirstOrDefaultAsync(p => p.Id == portfolio.Id);
        if (ptf == null) return NotFound();


        ptf.UserId = portfolio.UserId;
        ptf.Title = portfolio.Title;
        ptf.Description = portfolio.Description;
        ptf.Link = portfolio.Link;

        _db.FreelancerPortfolios.Update(ptf);
        await _db.SaveChangesAsync();

        return Ok(new PortfolioResponse {
            Flag = true,
            Message = "Portfolio updated."
        });
    }


    // delete a portfolio
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePortfolio(int id) {
        var portfolio = await _db.FreelancerPortfolios.FirstOrDefaultAsync(p => p.Id == id);
        if (portfolio == null) return NotFound();

        _db.FreelancerPortfolios.Remove(portfolio);
        await _db.SaveChangesAsync();

        return Ok(new PortfolioResponse{
            Flag = true,
            Message = "Portfolio Deleted."
        });
    }



}
