using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TicketSystem.Data;
using TicketSystem.Helper;
using TicketSystem.Models.Authentication;

namespace TicketSystem.Authentication
{
    public class MyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UserManager<UserModel> userManager;
        private readonly ApplicationDbContext context;
        private readonly AppSettings appSettings;

        public MyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            UserManager<UserModel> userManager,
            ApplicationDbContext context,
            IOptions<AppSettings> appSettings) : base(options, logger, encoder, clock)
        {
            this.userManager = userManager;
            this.context = context;
            this.appSettings = appSettings.Value;
        }

        public StringValues JsonConerter { get; private set; }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
                var authorizationHeader = Context.Request.Headers["Authorization"];
                if (!authorizationHeader.Any())
                    return await Task.FromResult(AuthenticateResult.NoResult());

                var sessionid = authorizationHeader.ToString();
                if (string.IsNullOrWhiteSpace(sessionid))
                    return await Task.FromResult(AuthenticateResult.NoResult());

            var session = context.UserSessions.Include(s => s.User)
                                              .Where(s => s.SessionId == sessionid)
                                              .FirstOrDefault();
            if (session != null && session.User != null && session.LastAccess.AddMinutes(appSettings.SessionExpireTimeInMinute) >= DateTime.Now)
            {
                //Utolsó hozzáférés frissítése az adatbázisban
                session.LastAccess = DateTime.Now;
                context.Entry(session).State = EntityState.Modified;
                context.SaveChanges();

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, session.User.UserName),
                    new Claim(ClaimTypes.Email, session.User.Email),
                    new Claim("Id", session.User.Id)
                };

                //szerepek beolvasása és hozzáadása a claim listához
                var roles = await userManager.GetRolesAsync(session.User);
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));

                var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);

                var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties(), Scheme.Name);

                Context.Response.Headers.Add("session",
                    JsonConvert.SerializeObject(new 
                    {
                        id = session.User.Id,
                        email = session.User.Email,
                        token = session.SessionId,
                        username = session.User.UserName,
                        roles = roles,
                        lastAccess = DateTime.Now,
                        validTo = DateTime.Now.AddMinutes(appSettings.SessionExpireTimeInMinute)
                    }));

                return await Task.FromResult(AuthenticateResult.Success(ticket));

            }
            return await Task.FromResult(AuthenticateResult.Fail("Sikertelen authentikáció"));
        }
    }
}
