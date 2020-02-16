using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.Authentication
{
    public class UserModel: IdentityUser
    {
        [MaxLength(100)]
        [Required]
        public string SurName { get; set; }

        [MaxLength(100)]
        public string ForeName { get; set; }

        /// <summary>
        /// User will receive or not receive emails about the changes
        /// </summary>
        public bool SendEmail { get; set; }

        public IList<UserRoleModel> Roles { get; set; }
    }
}
