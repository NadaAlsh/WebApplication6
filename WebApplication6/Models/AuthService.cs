using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static WebApplication6.Controllers.LoginController;

namespace WebApplication6.Models
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (bool IsValid, string Token) GenerateToken(string username, string password)
        {
            if (username != "admin" && password != "admin")
            {
                return (false, "");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // This is the part I can change and play with depending on what I need, everything else is set
            var claims = new[]
            {
        new Claim(TokenClaimsConstant.Username, username),
        new Claim(TokenClaimsConstant.UserId, "1"),
        new Claim(ClaimTypes.Role, "User")
        };
            //
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),//Expire
                signingCredentials: credentials);
            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (true, generatedToken);
        }
    }
}
