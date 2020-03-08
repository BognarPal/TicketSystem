using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Models.Authentication;

namespace TicketSystem.Data
{
    //Forrás: https://stackoverflow.com/questions/34343599/how-to-seed-users-and-roles-with-code-first-migration-using-identity-asp-net-cor
    public class InitialData
    {

        private ApplicationDbContext context;

        public InitialData(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async void AddAdminUser()
        {
            var user = new UserModel()
            {
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@localhost.com",
                NormalizedEmail = "admin@localhost.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                SurName = "admin",
                SendEmail = false
            };

            if (!context.Users.Any( u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<UserModel>();
                user.PasswordHash = password.HashPassword(user, "Titk0s");
                var userStore = new UserStore<UserModel>(context);
                await userStore.CreateAsync(user);

                await context.SaveChangesAsync();
            }
            if (context.Users.Any(u => u.UserName == user.UserName))
            {
                user = context.Users.Where(u => u.UserName == user.UserName).First();
                if (!context.UserRoles.Any(ur => ur.User == user))
                {
                    context.UserRoles.Add(new UserRoleModel()
                    {
                        User = user,
                        RoleId = "1"
                    });
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
