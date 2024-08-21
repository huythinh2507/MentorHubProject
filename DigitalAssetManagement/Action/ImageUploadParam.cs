using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace YPP.MH.DigitalAssetManagement.Action
{
    public class ImageUploadParam : BaseParam
    {
        public required IFormFile File { get; set; }
        public string? Space { get; set; }
        public string? PublicId { get; set; }
        public string? Format { get; set; }
        public string? Type { get; set; }
        public string? NotificationUrl { get; set; }
        public string? Folder { get; set; }
        public virtual ResourceType ResourceType => ResourceType.Image;
        public override SortedDictionary<string, object> ToParamsDictionary()
        {
            var dict = base.ToParamsDictionary();
            AddParam(dict, "space", Space);
            AddParam(dict, "folder_name", Folder);
            AddParam(dict, "resource_type", ResourceType.ToString());
            AddParam(dict, "file", File);
            AddParam(dict, "public_id", PublicId);
            AddParam(dict, "format", Format);
            AddParam(dict, "type", Type);
            return dict;

        }


    }
}
