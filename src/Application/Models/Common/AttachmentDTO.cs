using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Mappings;
using Tawala.Application.Common.Utility;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Application.Models.Common
{
    public class AppAttachmentResDTO : IMapFrom<AppAttachment>
    {
        public Guid Id { get; set; }
        public string ImageId { get; set; }
        public string Name { get; set; }
       
        public string FullLink
        {
            get
            {
                if (Name != null)
                    return GlobalSetting.ImageUrl + Name;
                return Name;
            }
        }     
    }
    public class AppAttachmentFile : IMapFrom<AppAttachment>
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
