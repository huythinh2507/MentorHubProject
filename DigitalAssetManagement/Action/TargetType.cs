using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DigitalAssetManagement
{
    public enum TargetType
    {
        [EnumMember(Value = "tenant")]
        Tenant,

        [EnumMember(Value = "space")]
        Space,

        [EnumMember(Value = "folder")]
        Folder,
    }
}
