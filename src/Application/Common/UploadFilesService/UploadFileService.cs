using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawala.Application.Common.Attachments;
using Tawala.Application.Common.Exceptions;
using Tawala.Application.Services.Common.IService;
using Tawala.Domain.Entities.Settings;

namespace Tawala.Application.Common.UploadFilesService
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IFileConversion fileConversion;
        private readonly IAttachmentService attachmentService;

        public UploadFileService(IFileConversion fileConversion, IAttachmentService _attachmentService)
        {
            this.fileConversion = fileConversion;
            this.attachmentService = _attachmentService;
        }

        public async Task<Guid> UploadFile(string file, string fileName,string userId)
        {

            try
            {
                AppAttachment attachment = new AppAttachment();
                System.Guid guid = System.Guid.NewGuid();
                attachment.ImageId = guid;
                attachment.AppUserId = userId;
                var newName = guid.ToString() + "." + fileName.Split('.').Last();
                attachment.Name = newName;
                var x = fileConversion.SaveFileToPath(file, newName);
                attachment.Name = x;
                var res = await attachmentService.Add(attachment);
                return res.Id;
            }
            catch (Exception)
            {

                throw new CantAddException(exMessage: "حدث خطأ في رفع الملف");
            }
        }
    }
}
