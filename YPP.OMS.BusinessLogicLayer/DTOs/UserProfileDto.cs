using GraphQL.Types.Relay.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.BusinessLogicLayer.DTOs
{
    public class UserProfileDto
    {
        public User User { get; set; }
        public List<Post> Posts { get; set; }
        public List<Post> LikedPosts { get; set; }
        public int SpacesCount { get; set; }
        public List<SpaceInfo> Spaces { get; set; }
    }

    public class SpaceInfo
    {
        public string Name { get; set; }
        public int MemberCount { get; set; }
    }
}
