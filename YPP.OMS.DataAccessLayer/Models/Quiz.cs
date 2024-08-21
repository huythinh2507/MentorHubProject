using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Quiz")]
    public class Quiz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        // Foreign key reference to the Sections table
        public int SectionId { get; set; }

        public bool EnableComment { get; set; } = false;

        public int PassingGrade { get; set; } = 0;

        public bool EnforcePassingGrade { get; set; } = false;

        public bool HideAnswer { get; set; } = false;

        public bool IsPublished { get; set; } = false;

        // Navigation property
        [ForeignKey("SectionsId")]
        public virtual Section? Section { get; set; }
    }
}
