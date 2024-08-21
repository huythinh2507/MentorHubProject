using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DataAccessLayer.Models
{
    [Table("SourceFilePath")]
    public class SourceFilePath
    {
        [Key]
        public int Id { get; set; }
        public int SourceId { get; set; }
        public string? SourceType { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
}
