using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class WorkspaceMemberDto
    {
        public UserDto User { get; set; } = new UserDto();
        public SpaceDto Space { get; set; } = new SpaceDto();
    }
}
