using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Invitation")]
    public class Invitation
    {
        public int Id { get; set; }
        public int SpaceId { get; set; }
        public int? TenantId { get; set; }
        public string? UserEmail { get; set; }
        public string? UserName { get; set; }
        public DateTime InvitationDate { get; set; } = DateTime.Now;
        public string Role { get; set; } = string.Empty;

    }
}
