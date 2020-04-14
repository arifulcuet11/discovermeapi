using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Interfaces
{
    public interface IAzureStorage
    {
        string UploadFileAsync(string strFileName, byte[] fileData, string fileMimeType);
        Task<string> EditBlobAsync(string containerName, string prefix);
        Task DeleteBlobData(string fileUrl);
        Task<List<string>> getListOfFileAsync(string containerName, string prefix, int id);
    }
}
