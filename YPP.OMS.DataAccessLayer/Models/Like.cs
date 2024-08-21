using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Like")]
    public class Like
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? PostId { get; set; }

        public int? CommentId { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;

        [ForeignKey(nameof(CommentId))]
        public virtual Comment Comment { get; set; } = null!;
    }
}
