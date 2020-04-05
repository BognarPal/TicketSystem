using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.Authentication
{
    public class UserStateHistoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public UserModel User { get; set; }

        [Required]
        public UserStateModel State { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
