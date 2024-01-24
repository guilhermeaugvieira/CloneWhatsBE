using CloneWhatsBE.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloneWhatsBE.Auth
{
    
    [ApiController, Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettingsOptions _jwtSettings;

        public AuthController(JwtSettingsOptions jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        public IActionResult Login(AuthLoginRequest request)
        {
            if (!UserFakeDb.Users.Any(usr => usr.Id == request.UserId))
                return NotFound("Usuário não encontrado");

            var byteSecret = Encoding.UTF8.GetBytes(_jwtSettings.Secret).ToArray();

            var secretKey = new SigningCredentials(
                new SymmetricSecurityKey(byteSecret), 
                SecurityAlgorithms.HmacSha256);

            var userIdClaim = new Claim(ClaimTypes.NameIdentifier, request.UserId.ToString().ToUpperInvariant());

            var securityToken = new JwtSecurityToken(
                signingCredentials: secretKey,
                claims: [userIdClaim]);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return Ok(new {
                request.UserId,
                token,
            });
        }
    }
}
