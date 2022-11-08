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

    public TransactionController(ILogger<TransactionController> logger)
    {
        _logger = logger;
        _service = new TransactionService();
    }

    [HttpPost("transaction")]
    public ActionResult<string> Transfer([FromBody] JsonElement json) {
        int? fromAccount = JsonSerializer.Deserialize<int>(json.GetProperty("from"));
        int? toAccount = JsonSerializer.Deserialize<int>(json.GetProperty("to"));
        decimal? amount = JsonSerializer.Deserialize<decimal>(json.GetProperty("amount"));

        if (fromAccount != null && toAccount != null && amount != null) {
            bool? successful = _service.TransferMoney((int) fromAccount, (int) toAccount, (decimal) amount);

            if (successful == null) return BadRequest("Unrecognized Account");
            else if (successful == false)   return BadRequest("ERROR!!!");
            else    return Created("", "Transaction recorded");
        }
        else    return BadRequest("Invalid Input");
    }
}