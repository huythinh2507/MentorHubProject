using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Space")]
    public partial class Space
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        public int? Groupspaceid { get; set; }
        public int? Spacetypeid { get; set; }

        [InverseProperty("Space")]
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public virtual ICollection<SpaceMember> Members { get; set; } = new HashSet<SpaceMember>();
    }
}