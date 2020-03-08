using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Models.Authentication;

namespace TicketSystem.Models
{
    public class UserPartnerModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public UserModel User { get; set; }

        [Required]
        public int PartnerId { get; set; }
        public PartnerModel Partner { get; set; }
    }
}
