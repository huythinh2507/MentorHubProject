using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Icon { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? SpaceGroupId { get; set; }

        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(255)]
        public string ThumbnailImg { get; set; } = string.Empty;

        public bool IsPublished { get; set; } = false;

        public int? CategoryId { get; set; }

        // Navigation properties
        [ForeignKey("SpaceGroupId")]
        public virtual Space? Space { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CourseCategory? CourseCategory { get; set; }
        public bool IsAddAll { get; set; }
    }
}
