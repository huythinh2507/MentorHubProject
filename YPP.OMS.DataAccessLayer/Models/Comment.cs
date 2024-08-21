using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }  

        public int? PostId { get; set; }  
        public DateTime CommentedAt { get; set; }  = DateTime.Now;
        public virtual User? User { get; set; }
        public string Content { get; set; } = string.Empty;
        public virtual ICollection<Like> Likes { get; set; } = new HashSet<Like>();
        public int? ParentCommentId { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = new HashSet<Comment>();
        public virtual Comment? ParentComment { get; set; }
    }
}
