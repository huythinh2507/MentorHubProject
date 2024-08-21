using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("SpaceMember")]
    public class SpaceMember
    {
        [Key]
        [Column(Order = 0)]
        public int SpaceId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MemberId { get; set; }

        [ForeignKey("WorkspaceId")]
        public virtual Space Space { get; set; } = new Space();

        [ForeignKey("MemberId")]
        public virtual User Member { get; set; } = new User();
    }
}
