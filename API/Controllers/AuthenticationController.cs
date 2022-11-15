using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;

using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private AuthenticationService _service; 

    public AuthenticationController(ILogger<AuthenticationController> logger, AuthenticationService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("Register")]
    public ActionResult<string> RegisterUser([FromBody] JsonElement json) {
        string? username = JsonSerializer.Deserialize<string>(json.GetProperty("username"));
        string? password = JsonSerializer.Deserialize<string>(json.GetProperty("password"));

        if (username != null && password != null) {
            if (_service.Register(username, password)) {
                return Created("", "Profile Created Successfully!");
            }
            else    return BadRequest("Username already exists!");
        }
        else    return BadRequest("Invalid Credentials");
    }

    [HttpPost("Login")]
    public ActionResult<string> LoginUser([FromBody] JsonElement json) {
        string? username = JsonSerializer.Deserialize<string>(json.GetProperty("username"));
        string? password = JsonSerializer.Deserialize<string>(json.GetProperty("password"));

        if (username != null && password != null) {
            string? tokenString = _service.LogIn(username, password);

            if (tokenString != null) {
                return Ok(new { token = tokenString });
            }
            else    return BadRequest("Unrecognized Credentials");
        }
        else    return BadRequest("Invalid Credentials");
    }

    [HttpPut("Login")]
    [Authorize]
    public ActionResult<string> ResetLogin([FromBody] JsonElement json) {
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);

            string? oldUname = JsonSerializer.Deserialize<string>(json.GetProperty("username"));
            string? oldPass = JsonSerializer.Deserialize<string>(json.GetProperty("password"));

            string? newUname = JsonSerializer.Deserialize<string>(json.GetProperty("newUsername"));
            string? newPass = JsonSerializer.Deserialize<string>(json.GetProperty("newPassword"));

            if (oldUname != null && oldPass != null && newUname != null && newPass != null) {
                // Verify that old username & password given exists in DB
                if (!String.IsNullOrEmpty(_service.LogIn(oldUname, oldPass))) {
                    // Check if Username is to be changed
                    if (oldUname != newUname) {
                        if (!_service.ChangeLogin(oldUname, newUname, userId, false)) {
                            return BadRequest("Cannot change other's credentials!");
                        }
                    }
                    // Check if Password is to be changed
                    if (oldPass != newPass) {
                        if (!_service.ChangeLogin(oldUname, newPass, userId, true)) {
                            return BadRequest("Cannot change other's credentials!");
                        }
                    }

                    return Ok("LogIn Credentials Updated!");
                }
                else    return BadRequest("Current Credentials Not Matching");
            }
            else    return BadRequest("Invalid Credentials");
        }
        else    return BadRequest("User Not Logged In!");
    }
}