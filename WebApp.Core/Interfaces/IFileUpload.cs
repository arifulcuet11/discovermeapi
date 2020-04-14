using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Interfaces
{
    public interface IFileUpload
    {
        string UploadsingleFile(IFormFileCollection formFileCollection, string folder, string fileName);
    }
}
