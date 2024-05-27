using asp_net_restful_api_jwt.Authentication;
using asp_net_restful_api_jwt.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_restful_api_jwt.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            if (String.IsNullOrEmpty(user.Email))
            {
                return BadRequest(new { message = "Email address is required" });
            }
            else if (String.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { message = "Password is required" });
            }

            UserIdentity loggedInUser = await _authenticationService.Login(user.Email, user.Password);

            if (loggedInUser != null)
            {
                return Ok(loggedInUser);
            }

            return BadRequest(new { message = "User login failed!" });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegister user)
        {
            if (String.IsNullOrEmpty(user.Email))
            {
                return BadRequest(new { message = "Email address is required" });
            }
            else if (String.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { message = "Password is required" });
            }

            User userToRegister = new(user.FirstName, user.LastName, user.Email, user.Password, user.Role);

            UserIdentity registeredUser = await _authenticationService.Register(userToRegister);
            return Ok(registeredUser);
        }

    }
}
