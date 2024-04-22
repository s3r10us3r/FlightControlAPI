using FlightControl.DAL.Models;
using FlightControl.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FlightControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : FlightControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(UserService userService, TokenService tokenService, ILogger<AuthController> logger) : base(logger)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                User user = _userService.CreateOne(model.Login, model.Password);
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequestWError(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                User user = _userService.AuthenticateUser(model.Login, model.Password);
                string token = _tokenService.GenerateJWToken(user);
                return Ok(new { token = token });
            }
            catch (InvalidOperationException e)
            {
                return BadRequestWError(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteUser()
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.Sub))
            {
                string login = currentUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
                Console.WriteLine(login);
                User? user = _userService.ReadOne(login);
                if (user is null)
                {
                    return BadRequestWError("User does not exist!");
                }
                _userService.DeleteOne(user);
                return Ok();
            }

            return Unauthorized();
        }
    }

    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
