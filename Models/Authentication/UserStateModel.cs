using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.Authentication
{
    public class UserStateModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        public bool CanLogin { get; set; }

        public bool CanWrite { get; set; }

    }
}
