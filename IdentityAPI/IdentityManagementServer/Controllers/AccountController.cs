using System.Security.Claims;
using IdentityManagementInfrastructure.Persistence;
using IdentityManagementServer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagementServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("user/register")]
    public async Task<IActionResult> Register([FromBody]RegisterRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new AppUser
        {
            UserName = request.Email,
            Name = request.Name,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddClaimAsync(user, new Claim("userName", user.UserName));
        await _userManager.AddClaimAsync(user, new Claim("name", user.Name));
        await _userManager.AddClaimAsync(user, new Claim("email", user.Email));
        await _userManager.AddClaimAsync(user, new Claim("role", "user"));
        
        return Ok();

    }
}