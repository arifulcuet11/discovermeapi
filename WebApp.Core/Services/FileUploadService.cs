using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;

namespace WebApp.Core.Services
{
    public class FileUploadService : IFileUpload
    {
        public string UploadsingleFile(IFormFileCollection formFileCollection, string folder, string fileName)
        {
            var folderName = Path.Combine("Resources", "Images", folder);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            // var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fullPath = Path.Combine(pathToSave, fileName + Path.GetExtension(formFileCollection[0].FileName));
            var dbPath = Path.Combine(folderName, fileName + Path.GetExtension(formFileCollection[0].FileName));
            var has_directory = Directory.Exists(pathToSave);
            if (!has_directory)
                Directory.CreateDirectory(pathToSave);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                formFileCollection[0].CopyTo(stream);
            }

            return dbPath;
        }
    }
}
