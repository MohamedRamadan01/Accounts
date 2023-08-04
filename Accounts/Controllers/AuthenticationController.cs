using Accounts.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Accounts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto user)
        {
            if (user is null)
            {
                return BadRequest("Invalid user request");
            }


            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
           


            if (user.UserName.ToLower() == "admin" && user.Password.ToLower() == "admin")
            {
                var adminTokeOptions = new JwtSecurityToken(
                               issuer: "https://localhost:7177",
                               audience: "https://localhost:4200",
                               claims: new Claim[] { new Claim("Role", "Admin") },
                               expires: DateTime.Now.AddMinutes(5),
                               signingCredentials: signinCredentials
                           );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(adminTokeOptions);
                return Ok(new { Token = tokenString });
            }
            else if (user.UserName.ToLower() == "user" && user.Password.ToLower() == "user")
            {
                var userTokeOptions = new JwtSecurityToken(
                               issuer: "https://localhost:7177",
                               audience: "https://localhost:4200",
                               claims: new Claim[] { new Claim("Role", "User") },
                               expires: DateTime.Now.AddMinutes(5),
                               signingCredentials: signinCredentials
                           );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(userTokeOptions);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}