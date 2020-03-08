using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        ApplicationDbContext context;

        public AdminController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("partners")]
        [Authorize(Roles = "Admin")]
        public IActionResult Partners()
        {
            try
            {
                return Ok(context.Partners
                    .Include(u => u.UserPartners)
                    .Select(u => new PartnerViewModel()
                    {
                        Id = u.Id,
                        ShortName = u.ShortName,
                        Name = u.Name,
                        Active = u.Active,
                        EmailDomain = u.EmailDomain,
                        Users = u.UserPartners.Select(up => up.UserId).ToList()
                    }));
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.Message);
#else
                return BadRequest("Váratlan hiba...");
#endif
            }
        }

        [HttpGet("partner/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPartner(int id)
        {
            try
            {
                return Ok(context.Partners
                    .Where(p => p.Id == id)
                    .Include(u => u.UserPartners)
                    .Select(u => new PartnerViewModel()
                    {
                        Id = u.Id,
                        ShortName = u.ShortName,
                        Name = u.Name,
                        Active = u.Active,
                        EmailDomain = u.EmailDomain,
                        Users = u.UserPartners.Select(up => up.UserId).ToList()
                    }).FirstOrDefault());
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.Message);
#else
                return BadRequest("Váratlan hiba...");
#endif
            }
        }

        [HttpPost("updatepartner")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePartner([FromBody] PartnerViewModel formData)
        {
            try
            {
                var partner = context.Partners.Where(p => p.Id == formData.Id).Include(u => u.UserPartners).FirstOrDefault();
                if (partner != null)
                {
                    formData.CopyFieldsTo(partner);
                    context.Entry(partner).State = EntityState.Modified;
                    context.SaveChanges();
                    return GetPartner(partner.Id);
                }
                return BadRequest("Nem létező partner");
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.Message);
#else
                return BadRequest("Váratlan hiba...");
#endif
            }
        }

        [HttpPost("insertpartner")]
        [Authorize(Roles = "Admin")]
        public IActionResult InsertPartner([FromBody] PartnerViewModel formData)
        {
            try
            {
                var partner = formData.CopyFieldsTo(new PartnerModel());
                context.Partners.Add(partner);
                foreach (var userid in formData.Users)
                {
                    context.UserPartners.Add(new UserPartnerModel()
                    {
                        UserId = userid,
                        Partner = partner
                    });
                }
                context.SaveChanges();
                return GetPartner(partner.Id);
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.Message);
#else
                return BadRequest("Váratlan hiba...");
#endif
            }
        }

        [HttpPost("deletepartner")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePartner([FromBody] PartnerViewModel formData)
        {
            try
            {
                var partner = context.Partners.Find(formData.Id);
                context.Partners.Remove(partner);
                context.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(ex.Message);
#else
                return BadRequest("Váratlan hiba...");
#endif
            }
        }

        [HttpGet("usernames")]
        [Authorize(Roles = "Admin")]
        public IActionResult UserNames()
        {
            return Ok(context.Users.Select(u => new
            {
                u.Id,
                Name = u.UserName
            }));
        }
    }
}