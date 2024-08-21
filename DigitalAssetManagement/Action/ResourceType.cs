using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DigitalAssetManagement.Action
{
    public enum ResourceType
    {
        /// <summary>
        /// Images in various formats (jpg, png, etc.)
        /// </summary>
        [EnumMember(Value = "image")]
        Image,

        /// <summary>
        /// Any files (text, binary)
        /// </summary>
        [EnumMember(Value = "raw")]
        Raw,

        /// <summary>
        /// Video files in various formats (mp4, etc.)
        /// </summary>
        [EnumMember(Value = "video")]
        Video,
    }
}
