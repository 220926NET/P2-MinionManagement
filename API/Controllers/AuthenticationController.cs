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
    private readonly AuthenticationService _service; 

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
                _logger.LogInformation("New user registered, profile & tied checking/saving accounts created.");
                return Created("", 201);
            }
            else {
                _logger.LogWarning("Attempt to register with credentials that already exist.");
                return BadRequest(400);
            }
        }
        else    return BadRequest(400);
    }

    [HttpPost("Login")]
    public ActionResult<string> LoginUser([FromBody] JsonElement json) {
        string? username = JsonSerializer.Deserialize<string>(json.GetProperty("username"));
        string? password = JsonSerializer.Deserialize<string>(json.GetProperty("password"));

        if (username != null && password != null) {
            string? tokenString = _service.LogIn(username, password);

            if (tokenString != null) {
                _logger.LogInformation("Someone logged in!");     // Example of how to log something
                return Ok(new { token = tokenString });
            }
            else {
                _logger.LogError("Could not generate JWT Token");
                return BadRequest(400);
            }
        }
        else    return BadRequest(400);
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
                            _logger.LogWarning($"Attempt to change other user's credentials by UserID={userId}");
                            return BadRequest(400);
                        }
                    }
                    // Check if Password is to be changed
                    if (oldPass != newPass) {
                        if (!_service.ChangeLogin(oldUname, newPass, userId, true)) {
                            _logger.LogWarning($"Attempt to change other user's credentials by UserID={userId}");
                            return BadRequest(400);
                        }
                    }
                    _logger.LogInformation($"LogIn credentials updated for UserID={userId}");
                    return Ok(200);
                }
                else    return BadRequest(400);
            }
            else    return BadRequest(400);
        }
        else    return Unauthorized(401);
    }
}