using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;

using Services;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private AccountService _service; 

    public AccountController(ILogger<AccountController> logger, AccountService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("transaction")]
    public ActionResult<string> Transfer([FromBody] JsonElement json) {
        int? fromAccount = JsonSerializer.Deserialize<int>(json.GetProperty("from"));
        int? toAccount = JsonSerializer.Deserialize<int>(json.GetProperty("to"));
        decimal? amount = JsonSerializer.Deserialize<decimal>(json.GetProperty("amount"));

        if (fromAccount != null && toAccount != null && amount != null) {
            bool? successful = _service.TransferMoney((int) fromAccount, (int) toAccount, (decimal) amount);

            if (successful == null) return BadRequest(400);
            else if (successful == false)   return BadRequest(400);
            else    return Created("", 201);
        }
        else    return BadRequest(400);
    }

    [HttpGet("{accountNum}")]
    public ActionResult<string> Transactions(int accountNum) {
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            List<Dictionary<int, decimal>> transactions = _service.Transactions(accountNum, userId);

            if (transactions.Count > 0) 
                return Ok(JsonSerializer.Serialize<List<Dictionary<int,decimal>>>(transactions));
            else    return BadRequest("User is Not Account Owner!");
        }
        else    return BadRequest("User Not Logged In!");
    }
}