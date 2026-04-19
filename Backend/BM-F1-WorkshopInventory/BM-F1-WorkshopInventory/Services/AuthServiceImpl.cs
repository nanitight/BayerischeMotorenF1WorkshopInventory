
using BM_F1_WorkshopInventory.Data;
using BM_F1_WorkshopInventory.Interfaces;
using BM_F1_WorkshopInventory.Models.DTO;
using BM_F1_WorkshopInventory.Models.Entities;
using BM_F1_WorkshopInventory.Models.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BM_F1_WorkshopInventory.Services
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly BMF1DbContext context;
        IConfiguration configuration;
        public AuthServiceImpl(BMF1DbContext context,IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public async Task<string?> LoginAsync(UserDTO user)
        {
            var userDb = await context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            if (userDb is null)
            {
                throw new UserErrorException("User not found");
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(userDb, userDb.PasswordHash, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new UserErrorException("Password is incorrect.");
            }
            try
            {
                var token = CreateToken(userDb);

                return token;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User?> RegisterAsync(UserDTO user)
        {
            if (await context.Users.AnyAsync(u=>u.Username == user.Username))
            {
                return null;
            }
            User userNew = new User();

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(userNew, user.Password);

            userNew.Username = user.Username;
            userNew.PasswordHash = hashedPassword;

            context.Users.Add(userNew);
            await context.SaveChangesAsync();
            return userNew;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role),
            };

            var secretToken = Environment.GetEnvironmentVariable("TOKEN");
            if (secretToken == null) throw new Exception("Token not in env files");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tknDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tknDescriptor);
        }
    }
}
