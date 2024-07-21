using AbronalFreelance.Server.DTO;
using AbronalFreelance.Server.DTOs;
using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbronalFreelance.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<User>  _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO) {
        var user = new User {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            UserName = registerDTO.UserName,
            Email = registerDTO.Email,
            LocationId = registerDTO.LocationId,
            CreatedAt = DateTime.Now,
            IsActive = true
        };
        var result = await _userManager.CreateAsync(user, registerDTO.Password);
        if (result.Succeeded) {
            if (!await _roleManager.RoleExistsAsync(registerDTO.Role))
                return BadRequest(new { Error = "Invalid Role."});

            await _userManager.AddToRoleAsync(user, registerDTO.Role);

            return Ok(new { Result = "Registration Compeleted."});
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO) {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);

        if (user == null)
            return Unauthorized(new { Error = "Invalid login attempt."});

        var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDTO.Password, false, false);
        if (result.Succeeded) {
            var roles = await _userManager.GetRolesAsync(user);
            string redirectUrl = "";

            if (roles.Contains("Admin"))
                redirectUrl = "admin/";
            else if (roles.Contains("Client"))
                redirectUrl = "client/";
            else if (roles.Contains("Freelancer"))
                redirectUrl = "freelancer/";
            return Ok(new { RedirectUrl = redirectUrl + "Dashboard"});
        }
        return Unauthorized(new { Error = "Invalid login attempt."});
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("admin/Dashboard")]
    public IActionResult AdminDashboared() {
        return Ok("Admin Dashboard");
    }


}
