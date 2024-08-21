using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.DataAccessLayer.Models
{
    public class CreatePostView
    {
        public int OwnerId { get; set; }

        public int SpaceId { get; set; }

        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int? FileId { get; set; }

        public string? CoverImg { get; set; }

        public bool IsPublished { get; set; } = false;

        public DateTime TimeSchedule { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string? Link { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Id { get; set; }
        public bool? HideLikes { get; set; } = false;
        public bool? HideComments { get; set; } = false;
        public bool? CloseComments { get; set; } = false;
    }
}
