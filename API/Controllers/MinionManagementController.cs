using Microsoft.AspNetCore.Mvc;
using Services;
using Models;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MinionManagementController : ControllerBase
{


    private readonly ILogger<MinionManagementController> _logger;
    private readonly AccountTransactionService _AccTranservice;
    private readonly AccountCreateService _AccCreatservice;

    public MinionManagementController(ILogger<MinionManagementController> logger)
    {
        _logger = logger;
        _AccTranservice = new AccountTransactionService();
        _AccCreatservice = new AccountCreateService();
    }

    [HttpPut("UpdateAccountAmount")]
    public ActionResult<int> UpdateAccountAmount(Transaction transaction){

        //check if Model is valid
        if(!ModelState.IsValid){
            
            UnprocessableEntity(transaction);
        }
        else{

            //Model is valid, then call ToUpdateAccountAmount function from AccountService
            int returnAffectedRows = _AccTranservice.ToUpdateAccountAmount(transaction);
            // correct respond will be 2 affected rows (sender's and received's amount changed)
            if(returnAffectedRows == 2){

                // correct status code 204
                return NoContent();
            }
            else{

                // incorrect state code 404
                return NotFound("Something wrong");
            }
        }

        return NotFound();
    }


    [HttpPost("CreateAccount")]
    public ActionResult CreateAccount(Account account){

        if(!ModelState.IsValid){

            UnprocessableEntity(account);
        }
        else{

            int returnAffectedRows = _AccCreatservice.ToCreateAccount(account);

            if(returnAffectedRows == 1){

                return Created("", "Created Successfully");
            }
            else{

                return BadRequest("Faliure");
            }
        }

        return BadRequest("Outside Conditional");
    }



}
