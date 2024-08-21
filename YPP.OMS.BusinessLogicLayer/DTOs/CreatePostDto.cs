using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class CreatePostDto
    {
        public int? OwnerId { get; set; }
        public int? SpaceId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? FileId { get; set; }
        public int? CoverImg { get; set; }
        public bool? IsPublished { get; set; } = false;
        public DateTime? TimeSchedule { get; set; } 
        public string? Url { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
