using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestCarAPI.Models.Car.DTO;
using TestCarAPI.Models.User;
using TestCarAPI.Models.UserData;
using TestCarAPI.Repositories.Interfaces;

namespace TestCarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IUserRepository _userRepository { get; set; }


        public LoginController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var user = await Authenticate(userLogin);

                if (user == null)
                {
                    return NotFound("User was not found");
                }

                var token = GenerateToken(user);
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Login went unsuccessfully.");
            }
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserModel> Authenticate(UserLogin userLogin)
        {
            var user = await _userRepository.GetUserByLoginAndPasswordAsync(userLogin.UserName, userLogin.Password);
            return user;
        }
    }
}
