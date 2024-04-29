using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenService service;
        public LoginController(TokenService service, BankContext bankContext)
        {
            this.service = service;
            this.context = bankContext;
        }
        private readonly BankContext context;

       

        [HttpPost("login")]
        public IActionResult Login(UserLogin loginDetails)
        {
            var response = service.GenerateToken(loginDetails.Username, loginDetails.Password);
            if (response.IsValid)
            {
                return Ok(new { Token = response.Token});
            }
            return BadRequest("Username and/or Password is wrong");

        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegistration userRegistration)
        {
            bool isAdmin = false;
            if (context.Users.Count() == 0)
            {
                isAdmin = true;
            }

            var newAccount = UserAccount.Create(userRegistration.Username, userRegistration.Password, isAdmin);

            context.Users.Add(newAccount);
            context.SaveChanges();

            return Ok(new { Message = "User Created" });
        }

        public class UserLogin()
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class TokenClaimsConstant
        {
            public static readonly string Username = "Kfh.Username";
            public static readonly string UserId = "Kfh.UserId";
        }

    }
}


