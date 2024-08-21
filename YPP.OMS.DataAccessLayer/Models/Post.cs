using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using YPP.MH.Shared.BaseEntity;
using Newtonsoft.Json;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Post")]
    public partial class Post : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int OwnerId { get; set; } 

        public int SpaceId { get; set; }

        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int? FileId { get; set; }

        public string? CoverImg { get; set; }

        public bool? IsPublished { get; set; }

        public DateTime? TimeSchedule { get; set; }

        [StringLength(255)]
        public string? Link { get; set; }
        public virtual User? Owner { get; set; }

        [ForeignKey(nameof(SpaceId))]
        [InverseProperty(nameof(Space.Posts))]
        [JsonIgnore] public virtual Space Space { get; set; } = new Space();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new HashSet<Like>();
        public bool? HideLikes { get; set; } = false;
        public bool? HideComments { get; set; } = false;
        public bool? CloseComments { get; set; } = false;
    }

}