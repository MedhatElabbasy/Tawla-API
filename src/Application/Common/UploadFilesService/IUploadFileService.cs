using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.UploadFilesService
{
    public interface IUploadFileService
    {
        Task<Guid> UploadFile(string file, string fileName,string userId);
    }
}
