using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawala.Application.Common.UploadFilesService;
using Tawala.Application.Models.Common;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Tawala.WebUI.Controllers
{
    [ApiController]
    [Route("api/Attachment")]
    public class AttachmentController : ApiControllerBase
    {
        private readonly IUploadFileService uploadFileService;

        public AttachmentController(IUploadFileService _uploadFileService)
        {
            this.uploadFileService = _uploadFileService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<Guid> AddFile(FileUploadDTO file)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await uploadFileService.UploadFile(file.File, file.FileName, userId);
        }

        // [Authorize]
        [HttpPost]
        [Route("uploadFormFile")]
        public async Task<Guid> uploadFormFile([FromForm] IFormFile file)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string fileBase64 = "";
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                fileBase64 = Convert.ToBase64String(fileBytes);
            }

            if (fileBase64 != "")
                return await uploadFileService.UploadFile(fileBase64, file.FileName, userId);
            else
                return new Guid();
        }
    }
}
