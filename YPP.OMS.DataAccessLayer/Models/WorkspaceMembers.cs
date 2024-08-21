using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("WorkspaceMembers")]
    public partial class WorkspaceMember
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [ForeignKey(nameof(Space))]
        public int WorkspaceId { get; set; }
        public virtual Space Space { get; set; } = null!;

        public string Role { get; set; } = RoleType.Member.ToString();
    }
}
