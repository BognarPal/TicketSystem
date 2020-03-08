using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class PartnerModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        [Required]
        public string ShortName { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public bool Active { get; set; } = true;

        [StringLength(100)]
        public string EmailDomain { get; set; }

        public List<UserPartnerModel> UserPartners { get; set; }
    }
}
