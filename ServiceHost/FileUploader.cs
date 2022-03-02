using System;
using System.IO;
using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ServiceHost
{
    public class FileUploader:IFileUploader
    {

        private readonly IWebHostEnvironment _WebHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
        }


        public string Upload(IFormFile file,string path)
        {
            if (file == null) return "";

            var directoryPath = $"{_WebHostEnvironment.WebRootPath}//ProductPictures//{path}";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = DateTime.Now.ToFileName()+"-"+file.FileName;

            var filePath = $"{directoryPath}//{fileName}";

            using var outPut=File.Create(filePath);
           // outPut.Close();
            file.CopyTo(outPut);
            

           return $"{path}/{fileName}";
        }
    }
}
