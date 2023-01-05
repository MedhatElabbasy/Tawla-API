using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.Attachments
{
    public interface IFileConversion
    {
        void ConvertFileToBase64StringAndSaveToPath(string File, string FilePath);
        bool CheckIfFileBase64String(string File);
        string SaveFileToPath(string File, string FileName);
        string SetFileName(string File);
        string GetFilePath(string FileFolder, string FileName);
    }
}
