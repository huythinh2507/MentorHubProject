using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("SpaceGroup")]
    public partial class SpaceGroup
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty; 
        public int TenantId { get; set; } 
        public bool HideMemberCount { get; set; } = false;
        public bool HideFromNonMembers { get; set; } = false;
        public bool ShowJoinedSpaces { get; set; } = false;
        public bool AutoAddToNewSpaces { get; set; } = false;
        public bool AutoAddToGroupOnJoin { get; set; } = false;
    }

    
}
