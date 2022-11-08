using Microsoft.AspNetCore.Mvc;
using Services;
using Models;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{

    // Injection for Service layers
    private readonly ILogger<AdminController> _logger;
    private readonly AdminAddMoneyService _adminAddService;
    private readonly AdminRemoveMoneyService _adminRemoveService;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
        _adminAddService = new AdminAddMoneyService();
        _adminRemoveService = new AdminRemoveMoneyService();

        
    }

    [HttpPost("addmoney/allusers/{amount}")]
    public ActionResult<int> AdminAddMoneyToAllUsers(double amount){

        //check if amount input is valid
        if(amount <= 0){
            
            UnprocessableEntity("Amount must be greate than 0");
        }
        else{

            //Model is valid, then call Distribute Money to all user function
            int returnAffectedRows = _adminAddService.AddMoneyToAllUsers(amount);
            // correct respond will be  at least one affected row (update successfully)
            if(returnAffectedRows > 0){

                //create reponse header
                HttpContext.Response.Headers.Add("Role", "Admin");
                // correct status code 201
                return Created("","Action Successfully");
            }
            else{

                // incorrect state code 400
                return BadRequest("Something wrong");
            }
        }

        return BadRequest();
    }


    [HttpPost("removemoney/allusers/{amount}")]
    public ActionResult<int> AdminRemoveMoneyFromAllUsersdouble (double amount){

        //check if amount input is valid
        if(amount <= 0){
            
            UnprocessableEntity("Amount must be  0");
        }
        else{

            //Model is valid, then call Distribute Money to all user function
            int returnAffectedRows = _adminRemoveService.RemoveMoneyFromAllUsers(amount);
            // correct respond will be  at least one affected row (update successfully)
            if(returnAffectedRows > 0){

                //create reponse header
                HttpContext.Response.Headers.Add("Role", "Admin");
                // correct status code 201
                return Created("","Action Successfully");
            }
            else{

                // incorrect state code 400
                return BadRequest("Something wrong");
            }
        }

        return BadRequest();
    }


}
