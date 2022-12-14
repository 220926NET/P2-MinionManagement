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
    private readonly AccountService _service; 
    private readonly BuyTroopService _troopService;

    public AccountController(ILogger<AccountController> logger, AccountService service, BuyTroopService troopService)
    {
        _logger = logger;
        _service = service;
        _troopService = troopService;
    }

    [HttpPost("Transaction")]
    public ActionResult<string> Transaction([FromBody] JsonElement json) {
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
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
        else    return Unauthorized(401);
    }

    [HttpPost("buytroop")]
    public ActionResult<int> buytroop([FromBody] JsonElement json){
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            int? numOfTroop = JsonSerializer.Deserialize<int>(json.GetProperty("numOfTroop"));

            if(numOfTroop != null){
                int affectedRow = _troopService.BuyTroopFunc((int)userId, (int)numOfTroop);

                if(affectedRow == 1) return Created("",201);
                else return BadRequest(400);
            }
            else return BadRequest(400);
        }
        else    return Unauthorized(401);
    }

    [HttpGet("{accountNum}")]
    public ActionResult<string> Transactions(int accountNum) {
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            List<Tuple<int, decimal>> transactions = _service.Transactions(accountNum, userId);

            if (transactions.Count > 0) 
                return Ok(JsonSerializer.Serialize<List<Tuple<int,decimal>>>(transactions));
            else {
                _logger.LogWarning($"Unauthorized attemp to access transactions of account={accountNum} by UserID={userId}");
                return BadRequest(400);
            }
        }
        else    return Unauthorized(401);
    }

    [HttpGet("Raid")]
    public ActionResult<string> OpponentInfo() {
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            Tuple<int, string> opponent = _service.Opponent(userId);

            if (opponent != new Tuple<int, string>(0, ""))
                return Ok(JsonSerializer.Serialize<Tuple<int, string>>(opponent));
            else {
                _logger.LogError($"Unable to find raiding opponent for UserID={userId}");
                return BadRequest(400);
            }
        }
        else    return Unauthorized(401);
    }

    [HttpPut("Raid/{opponent}")]
    public ActionResult<string> ResolveRaid(int opponent) {
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            Tuple<bool, int, decimal> raidResult = _service.Raid(userId, opponent);

            if (raidResult != new Tuple<bool, int, decimal>(false, 0, 0.0m))
                return Ok(JsonSerializer.Serialize<Tuple<bool, int, decimal>>(raidResult));
            else {
                _logger.LogWarning($"Unable to resolve raid by UserID={userId} on opponentID={opponent}");
                return BadRequest(400);
            }
        }
        else    return Unauthorized(401);
    }

    [HttpPut("endMonth")]
    public ActionResult<string> ResolveMonth() {
        if (HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Sid)) {
            int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            Tuple<decimal, int, int> monthReport = _service.MonthlyReport(userId);

            if (monthReport != new Tuple<decimal, int, int>(0.00m, 0, 0))
                return Ok(JsonSerializer.Serialize<Tuple<decimal, int, int>>(monthReport));
            else {
                _logger.LogError($"Unable to resolve monthly income/expenses for UserID={userId}");
                return BadRequest(400);
            }
        }
        else    return Unauthorized(401);
    }
}
