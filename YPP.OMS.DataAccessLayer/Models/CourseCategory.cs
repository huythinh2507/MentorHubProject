using System.ComponentModel.DataAnnotations.Schema;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("CourseCategory")]
    public class CourseCategory
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
    }
}