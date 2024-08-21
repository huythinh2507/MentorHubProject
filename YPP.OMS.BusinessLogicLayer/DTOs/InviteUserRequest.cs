﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class InviteUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int SpaceId { get; set; }
        public int TenantId { get; set; }
        
    } 
}
