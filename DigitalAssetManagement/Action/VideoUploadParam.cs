using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DigitalAssetManagement.Action
{
    public class VideoUploadParam : ImageUploadParam
    {
        public override ResourceType ResourceType => ResourceType.Video;
    }
}
