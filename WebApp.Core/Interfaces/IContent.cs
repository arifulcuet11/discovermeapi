using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entities;
using WebApp.Domain.VM;

namespace WebApp.Core.Interfaces
{
    public interface IContent : IGenericRepository<Content>
    {
        Task<ContentSearchVm> SearchAsync(string text, int index, int size, int contentTypeId);
        Task<List<ContentMbVm>> SearchAsync(string text, int index, int size, int contentTypeId, int userId);
        Task<ContentSearchVm> SearchAdminAsync(string text, int index, int size, int contentTypeId, int catagoryId, int statusId);
        Task<string> AproveOrDisAprove(long id, int statusId);
        Task<List<Content>> GetByContentTypeId(long contentTypeId);
        Task<List<ContentViewMobile>> SearchMobileAsync(string text, int index, int size, int contentTypeId);
        Task<List<ContentViewMobile>> DeshboardContent();
        Task<List<ContentViewMobile>>RecentlyAddedContent(int index, int size);
    }
}
