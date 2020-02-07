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
using TicketSystem.Data;
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
        private readonly ApplicationDbContext context;
        private static Random random = new Random();

        public AuthController(
            UserManager<UserModel> userManager, 
            IOptions<AppSettings> appSettings,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] LoginModel formData)
        {
            var user = await userManager.FindByEmailAsync(formData.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, formData.Password))
            {
                //sessionid létrehozása
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string sessionId;
                do
                    sessionId = new string(Enumerable.Repeat(chars, 120).Select(s => s[random.Next(s.Length)]).ToArray());
                while (context.UserSessions.Any(s => s.SessionId == sessionId));

                //új sessionid adatbázisba írása
                var now = DateTime.Now;
                context.UserSessions.Add(new UserSessionModel()
                {
                    SessionId = sessionId,
                    User = user,
                    LastAccess = now
                });

                //ha volt a felhasználónak korábbi, már lejárt session-je, annak törlése
                context.UserSessions.RemoveRange(
                    context.UserSessions.Where( s => s.LastAccess.AddMinutes(appSettings.SessionExpireTimeInMinute) < now));
                
                context.SaveChanges();

                var roles = await userManager.GetRolesAsync(user);
                
                return Ok(new
                {
                    id = user.Id,
                    email = user.Email,
                    token = sessionId,
                    username = user.UserName,
                    roles = roles,
                    lastAccess = now
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