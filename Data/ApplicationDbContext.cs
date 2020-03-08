using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Models;
using TicketSystem.Models.Authentication;

namespace TicketSystem.Data
{
    public class ApplicationDbContext: IdentityDbContext< UserModel,
                                                          IdentityRole,
                                                          string,
                                                          IdentityUserClaim<string>,
                                                          UserRoleModel,
                                                          IdentityUserLogin<string>,
                                                          IdentityRoleClaim<string>,
                                                          IdentityUserToken<string>>
    {
        public DbSet<UserStateModel> UserState { get; set; }
        public DbSet<UserStateHistoryModel> UserStateHistory { get; set; }
        public DbSet<UserSessionModel> UserSessions { get; set; }
        public DbSet<PartnerModel> Partners { get; set; }
        public DbSet<UserPartnerModel> UserPartners { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRoleModel>().HasOne(ur => ur.User)
                .WithMany(r => r.Roles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "0", Name = "EveryBody", NormalizedName = "EveryBody" },
                new IdentityRole() { Id = "1", Name = "Admin", NormalizedName = "Admin" },
                new IdentityRole() { Id = "2", Name = "Programmer", NormalizedName = "Programmer" },
                new IdentityRole() { Id = "3", Name = "Customer", NormalizedName = "Customer" }
            );

            builder.Entity<UserSessionModel>().HasIndex(s => s.SessionId).IsUnique();

            builder.Entity<PartnerModel>().HasIndex(p => p.Name).IsUnique();
            builder.Entity<PartnerModel>().HasIndex(p => p.ShortName).IsUnique();

            builder.Entity<UserPartnerModel>().HasIndex(up => new { up.PartnerId, up.UserId }).IsUnique();
        }
    }
}
