using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class CommentDto
    {
        public string Content { get; set; } = string.Empty;
        public int? UserId { get;  set; }
        public DateTime CommentedAt { get; internal set; }
        public int LikeCount { get; internal set; }
        public int Id { get; internal set; }
        public List<CommentDto> Replies { get; internal set; } = [];
        public string? UserName { get; internal set; }
        public string? UserProfileImg { get; internal set; }
    }
}
