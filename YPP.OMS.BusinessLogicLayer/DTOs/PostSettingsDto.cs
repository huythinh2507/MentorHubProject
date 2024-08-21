using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class PostSettingsDto
    {
        public bool HideLikes { get; set; }
        public bool HideComments { get; set; }
        public bool CloseComments { get; set; }
    }
}
