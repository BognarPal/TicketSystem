using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Models.Authentication;

namespace TicketSystem.Data
{
    public class ApplicationDbContext: IdentityDbContext<UserModel>
    {
        public DbSet<UserStateModel> UserState { get; set; }
        public DbSet<UserStateHistoryModel> UserStateHistory { get; set; }
        public DbSet<UserSessionModel> UserSessions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "0", Name = "EveryBody", NormalizedName = "EveryBody" },
                new IdentityRole() { Id = "1", Name = "Admin", NormalizedName = "Admin" },
                new IdentityRole() { Id = "2", Name = "Programmer", NormalizedName = "Programmer" },
                new IdentityRole() { Id = "3", Name = "Customer", NormalizedName = "Customer" }
            );

            builder.Entity<UserSessionModel>().HasIndex(s => s.SessionId).IsUnique();
        }
    }
}
