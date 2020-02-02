using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Models.Authentication;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserModel> userManager;

        public AuthController(UserManager<UserModel> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] LoginModel formData)
        {
            var user = await userManager.FindByEmailAsync(formData.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, formData.Password))
            {
                return Ok();
            }
            ModelState.AddModelError("", "Hibás felhasználó név vagy jelszó");
            return Unauthorized(new { error = "Hibás felhasználói név vagy jelszó" });
        }
    }
}