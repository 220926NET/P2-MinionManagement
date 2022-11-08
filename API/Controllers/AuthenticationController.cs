using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private AuthenticationService _service; 

    public AuthenticationController(ILogger<AuthenticationController> logger)
    {
        _logger = logger;
        _service = new AuthenticationService();
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
}