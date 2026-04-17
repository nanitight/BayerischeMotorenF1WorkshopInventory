using BM_F1_WorkshopInventory.Models.DTO;
using BM_F1_WorkshopInventory.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BM_F1_WorkshopInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        public static User user = new User();

        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO req)
        {
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, req.Password);

            user.Username = req.Username;
            user.PasswordHash = hashedPassword;

            return Ok(user);
        }


        [HttpPost("login")]
        public ActionResult<string> Login(UserDTO req)
        {
            if (user.Username != req.Username)
            {
                return BadRequest("User not found.");
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user,user.PasswordHash,req.Password) == PasswordVerificationResult.Failed)
            {
                return BadRequest("Password is incorrect.");
            }

            var token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tknDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tknDescriptor);
        }
    }
}
