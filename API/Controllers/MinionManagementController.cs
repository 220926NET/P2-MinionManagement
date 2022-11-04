using Microsoft.AspNetCore.Mvc;
using Services;
using Models;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MinionManagementController : ControllerBase
{


    private readonly ILogger<MinionManagementController> _logger;
    private readonly AdminDistributeMoneyService _adminService;

    public MinionManagementController(ILogger<MinionManagementController> logger)
    {
        _logger = logger;
        _adminService = new AdminDistributeMoneyService();
        
    }

    [HttpPost("admin/distributemoney/{amount}")]
    public ActionResult<int> UpdateAccountAmount(double amount){

        //check if amount input is valid
        if(amount <= 0){
            
            UnprocessableEntity("Amount must be larget than 0");
        }
        else{

            //Model is valid, then call Distribute Money to all user function
            int returnAffectedRows = _adminService.DistributeMoneyToAllUsers(amount);
            // correct respond will be  at least one affected row (update successfully)
            if(returnAffectedRows > 0){

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




}
