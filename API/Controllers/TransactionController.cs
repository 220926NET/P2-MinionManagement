using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private TransactionService _service;

    private BuyTroopService _troopService; 

    public TransactionController(ILogger<TransactionController> logger, TransactionService service, BuyTroopService troopService)
    {
        _logger = logger;
        _service = service;
        _troopService = troopService;
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

    [HttpPost("buytroop")]
    public ActionResult<int> buytroop([FromBody] JsonElement json){
        int? userID = JsonSerializer.Deserialize<int>(json.GetProperty("userID"));
        int? numOfTroop = JsonSerializer.Deserialize<int>(json.GetProperty("numOfTroop"));

        if(userID != null && numOfTroop != null){
            int affectedRow = _troopService.BuyTroopFunc((int)userID, (int)numOfTroop);

            if(affectedRow == 1) return Created("",201);
            else return BadRequest(400);
        }
        else return BadRequest(400);
    }
}