using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.Authentication
{
    public class UserSessionModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public UserModel User { get; set; }

        [Required]
        [StringLength(120)]
        public string SessionId { get; set; }

        public DateTime LastAccess { get; set; }
    }
}
