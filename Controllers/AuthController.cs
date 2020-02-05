using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicketSystem.Helper;
using TicketSystem.Models.Authentication;

namespace TicketSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserModel> userManager;
        private readonly AppSettings appSettings;

        public AuthController(UserManager<UserModel> userManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] LoginModel formData)
        {
            var user = await userManager.FindByEmailAsync(formData.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, formData.Password))
            {
                //token létrehozása
                var roles = await userManager.GetRolesAsync(user);
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                        new Claim("LoggedOn", DateTime.Now.ToString())
                    }),
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
                    Issuer = appSettings.Site,
                    Audience = appSettings.Audience,
                    Expires = DateTime.UtcNow.AddMinutes(double.Parse(appSettings.ExpireTime))
                };
                
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    expiration = token.ValidTo,
                    username = user.UserName,
                    userRole = roles.FirstOrDefault()
                });
            }
            ModelState.AddModelError("", "Hibás felhasználó név vagy jelszó");
            return Unauthorized(new { error = "Hibás felhasználói név vagy jelszó" });
        }

        [HttpGet("Teszt")]
        [Authorize(Roles = "Admin")]
        public IActionResult Teszt()
        {
            return Ok(new
            {
                message = "Siker"
            });
        }
    }
}