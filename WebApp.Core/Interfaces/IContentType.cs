using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entities;
using WebApp.Domain.VM;

namespace WebApp.Core.Interfaces
{
    public interface IContentType : IGenericRepository<ContentType>
    {
        Task<ContentTypeVm> SearchAsync(string text, int index, int size, int catagoryId);
    }
}
