using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RocaWebApi.Api.Config;
using RocaWebApi.Api.Data;
using RocaWebApi.Api.Features.Users;

namespace RocaWebApi.Api.Features.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JwtSettingsOptions _jwtSettingsOptions;

        public AuthController(ApplicationDbContext dbContext, IOptions<JwtSettingsOptions> jwtSettings)
        {
            _dbContext = dbContext;
            _jwtSettingsOptions = jwtSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthRequest authRequest)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == authRequest.Username);

            if (user == null)
            {
                return Unauthorized(new ProblemDetails
                {
                    Detail = "Nome de usuário ou senha estão incorretos"
                });
            }

            var token = GenerateJwtToken(user);

            return Ok(new AuthResponse(user, token));
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettingsOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_jwtSettingsOptions.ExpiresInDays),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
