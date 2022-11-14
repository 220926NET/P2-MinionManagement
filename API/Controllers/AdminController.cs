using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    // Injection for Service layers
    private readonly ILogger<AdminController> _logger;
    private readonly AdminService _adminService;

    public AdminController(ILogger<AdminController> logger, AdminService service)
    {
        _logger = logger;
        _adminService = service; 
    }

    [HttpPost("addmoney")]
    public ActionResult<int> AdminAddMoneyToAllUsers(decimal amount){
        //check if amount input is valid
        if(amount <= 0){
            UnprocessableEntity("Amount must be greate than 0");
        }
        else{
            //Model is valid, then call Distribute Money to all user function
            int returnAffectedRows = _adminService.AddMoneyToAllUsers(amount);
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


    [HttpPost("removemoney")]
    public ActionResult<int> AdminRemoveMoneyFromAllUsersdouble(decimal amount){
        //check if amount input is valid
        if(amount <= 0){
            UnprocessableEntity("Amount must be  0");
        }
        else{
            //Model is valid, then call Distribute Money to all user function
            int returnAffectedRows = _adminService.RemoveMoneyFromAllUsers(amount);
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
