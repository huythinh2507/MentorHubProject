using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Lesson")]
    public class Lesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        // Foreign key reference to the Sections table
        public int SectionId { get; set; }

        [StringLength(255)]
        public string? MediaVideo { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool EnableMedia { get; set; } = false;

        public bool EnableComment { get; set; } = false;

        public bool VideoCompletion { get; set; } = false;

        public bool EnableNextLesson { get; set; } = false;

        [Required]
        public string? DefaultTab { get; set; } 

        public bool IsPublished { get; set; } = false;

        // Navigation property
        [ForeignKey("SectionId")]
        public virtual Section? Section { get; set; }
    }

    public enum DefaultTab
    {
        Comments,
        Curriculum,
        Files
    }
}
