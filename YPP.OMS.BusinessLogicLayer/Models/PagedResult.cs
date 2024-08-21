using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPP.MH.BusinessLogicLayer.DTOs;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.BusinessLogicLayer.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class PostDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string SpaceName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int? FileId { get; set; }
        public string? CoverImg { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? TimeSchedule { get; set; }
        public string? Link { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<CommentDto> Comments { get; set; } = new HashSet<CommentDto>();
        public string? OwnerProfileImg { get; set; }
        public string? SpaceRole { get; set; } = string.Empty;
        public IEnumerable<User> LikedByUsers { get; set; } = [];
        public string? OwnerName { get; internal set; }
        public int CommentsCount { get; set; }
        public int SpaceId {  get; set; }
    }
}
