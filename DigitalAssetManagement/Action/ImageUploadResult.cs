using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DigitalAssetManagement.Action
{
    public class ImageUploadResult
    {
        public string? PublicId { get; set; }

        public ResourceType? ResourceType { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? OriginalFilename { get; set; }

        public string? SecureUrl { get; set; }
    }
}
