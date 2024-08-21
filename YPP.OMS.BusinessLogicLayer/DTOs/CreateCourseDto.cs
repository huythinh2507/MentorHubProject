using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class CreateCourseDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        public int? SpaceId { get; set; }

        [Required]
        public CourseType CourseType { get; set; } 
        public bool IsAddAll { get; set; } = false;
        public string Icon { get; internal set; } = "😊";
    }

}
