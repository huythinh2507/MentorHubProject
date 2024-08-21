using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class ReplyDto
    {
        public int UserId { get; set; }
        public string Content { get; set; } = string.Empty;
    }

}
