
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tawala.Application.Common.Attachments
{
    public class FileConversion : IFileConversion
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration configuration;
        public FileConversion(IWebHostEnvironment hostingEnvironment, IConfiguration _configuration)
        {
            configuration = _configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public bool CheckIfFileBase64String(string File)
        {
            return (File.Length % 4 == 0) && Regex.IsMatch(File, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public void ConvertFileToBase64StringAndSaveToPath(string File, string FilePath)
        {
            var FileBytes = Convert.FromBase64String(File);
            System.IO.File.WriteAllBytes(FilePath, FileBytes);
        }

        public string SetFileName(string File)
        {
            return String.Format(File);
        }

        public string SaveFileToPath(string File, string FileName)
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
           //  string contentRootPath = _hostingEnvironment.ContentRootPath;//+"\\"+ configuration.GetSection("PathSetting:Images").Value;
           string contentRootPath =   configuration.GetSection("AttachmentPath:AttachmentPath").Value;
            var isFileBase64String = CheckIfFileBase64String(File.Trim());

            if (!isFileBase64String)
                return Path.GetFileName(File);
            var imgName = SetFileName(FileName);

            var imgPath = Path.Combine(contentRootPath, imgName);
            ConvertFileToBase64StringAndSaveToPath(File, imgPath);
            return imgName;
        }

        public string GetFilePath(string FileFolder, string FileName)
        {
            var path = configuration.GetSection("PathSetting:Images").Value + FileFolder + "/" + FileName;
            return path.Replace(@"\\", @"/");
        }
    }
}
