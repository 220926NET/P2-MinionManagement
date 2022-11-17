using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;
using Models;

using Services;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;
    private readonly ProfileService _service; 

    public ProfileController(ILogger<ProfileController> logger, ProfileService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public ActionResult<string> GetUserInfo() {
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            User? userProfile = _service.ProfileInfo(userId);

            if (userProfile == null) {
                return NotFound("404");
            }
            else {
                userProfile = _service.AccountsInfo(userProfile, userId);
                return Ok(JsonSerializer.Serialize<User>(userProfile));
            }
        }
        else    return BadRequest("400");
    }

    [HttpPut]
    public ActionResult<string> EditInfo([FromBody] JsonElement json) {
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            User? updatedInfo = JsonSerializer.Deserialize<User>(json)!;

            if (updatedInfo != null) {
                User? newProfile = _service.ChangeInfo(userId, updatedInfo);

                if (newProfile != null)
                    return Ok(JsonSerializer.Serialize<User>(newProfile));
                else
                    return BadRequest("Unable to Implement Changes");
            }
            else    return BadRequest("Invalid Changes");
        }
        else    return BadRequest("User Not Logged In!");
    }
}