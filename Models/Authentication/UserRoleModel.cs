using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.Authentication
{
    public class UserRoleModel: IdentityUserRole<string>
    {
        public virtual UserModel User { get; set; }
    }
}
