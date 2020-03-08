using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class PartnerViewModel
    {
        public int? Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; } = true;
        public string EmailDomain { get; set; }

        public IEnumerable<string> Users { get; set; }

        public PartnerModel CopyFieldsTo(PartnerModel partner)
        {
            partner.ShortName = this.ShortName;
            partner.Name = this.Name;
            partner.Active = this.Active;
            partner.EmailDomain = this.EmailDomain;

            if (this.Id != null)
            {
                foreach (var user in partner.UserPartners.ToList())
                    if (!this.Users.Contains(user.UserId))
                        partner.UserPartners.Remove(user);

                foreach (var userid in this.Users)
                    if (!partner.UserPartners.Any(up => up.UserId == userid))
                        partner.UserPartners.Add(new UserPartnerModel()
                        {
                            PartnerId = partner.Id,
                            UserId = userid
                        });
                return partner;
            }
            return partner;
        }
    }
}
