using BM_F1_WorkshopInventory.Interfaces;
using BM_F1_WorkshopInventory.Models.DTO;
using BM_F1_WorkshopInventory.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BM_F1_WorkshopInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO req)
        {
            try
            {
                var user = await service.RegisterAsync(req);
                if (user == null) return BadRequest("Username already used.");
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO req)
        {
            try
            {
                var token = await service.LoginAsync(req);
                if (token == null) return BadRequest("Invalid username or password.");
                return Ok(token);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("authed")]
        public ActionResult<string> Test()
        {
            return "Authed";
        }
    }
}
