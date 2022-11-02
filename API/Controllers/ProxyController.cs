using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProxyController : ControllerBase
    {
        ServiceLayer service = new ServiceLayer();
        [HttpGet("LoginMinion/{username}/{password}")]
        public async Task<ActionResult<ProxyUserProfile>> LoginUser(string username, string password) {
               ProxyUserProfile ret = await service.LoginUser(username, password);
            return Created("Made it", ret);
        }
         [HttpPost("RegisterMinion")]
        public async Task<ActionResult<ProxyUserProfile>> RegisterUser(ProxyUserProfile user) {
              ProxyUserProfile ret = await service.RegisterANewUser(user);
            return Created("Made it", ret);
        }
    }
}