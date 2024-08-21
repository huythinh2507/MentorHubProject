using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("Section")]
    public class Section
    {
        [Key]
        public int Id { get; set; }

        public int? CourseId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
