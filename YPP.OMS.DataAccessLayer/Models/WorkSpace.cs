using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Workspace")]
    public class WorkSpace
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string? Name { get; set; }

        public int? OwnerId { get; set; }

        public int? CoverImg { get; set; }
    }
}
