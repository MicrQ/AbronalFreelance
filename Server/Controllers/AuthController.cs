using AbronalFreelance.Shared.Models;
using AbronalFreelance.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace AbronalFreelance.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole>  _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            IConfiguration configuration,
                            RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO) {
        if (!await _roleManager.RoleExistsAsync(registerDTO.Role))
            return BadRequest(new { Error = "Invalid Role."});
        
        var user = new User {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            UserName = registerDTO.UserName,
            Email = registerDTO.Email,
            LocationId = registerDTO.LocationId,
            PhoneNumber = registerDTO.PhoneNumber,
            CreatedAt = DateTime.Now,
            IsActive = true
        };
        var result = await _userManager.CreateAsync(user, registerDTO.Password);
        if (result.Succeeded) {
            await _userManager.AddToRoleAsync(user, registerDTO.Role);

            // if (registerDTO.Role == "Client") {
            //     var client = new Clientt {
            //         UserId = user.Id,
            //         CompanyName = registerDTO.CompanyName,
            //         CompanyLocationId = registerDTO.CompanyLocationId,
            //         TinNo = registerDTO.TinNo,
            //         CompanyEstablishedAt = registerDTO.CompanyPublished_at
            //     };
            // }

            return Ok(new {
                Message = "Registration Compeleted.",
                RedirectUrl = "login"
            });
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO) {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);

        if (user == null)
            return Unauthorized(new { Error = "Invalid login attempt."}); 
            // should be login errrorr !badreq

        var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDTO.Password, false, false);
        if (result.Succeeded) {
            var roles = await _userManager.GetRolesAsync(user);
            string redirectUrl = "";

            var role = "";
            if (roles.Contains("Admin")) {
                redirectUrl = "admin";
                role = "Admin";
            }
            else if (roles.Contains("Client")){
                redirectUrl = "client";
                role = "Client";
            }
            else if (roles.Contains("Freelancer")){
                redirectUrl = "freelancer";
                role = "Freelancer";
            }

            var token = GetJWTtoken(user, role);
            return Ok(new {
                Token = token,
                RedirectUrl = redirectUrl
            });
        }
        return Unauthorized(new { Error = "Invalid login attempt."});
    }

    private string GetJWTtoken(IdentityUser user, string role) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    // logout
    [HttpPost("logout")]
    public async Task<IActionResult> Logout() {
        await _signInManager.SignOutAsync();
        return Ok();
    }


}
