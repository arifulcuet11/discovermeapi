using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Domain.Entities;
using WebApp.Domain.VM;
using WebApp.Infrastructure.Context;
using WebApp.Utility;

namespace WebApp.Core.Services
{
    public class ContentService : GenericRepository<Content>, IContent
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IContentLike contentLikeService;
        private readonly IMarkAsRead markAsReadService;
        public ContentService(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
            contentLikeService = new ContentLikeService(context);
            markAsReadService = new MarkAsReadService(context);
        }

        public async Task<string> AproveOrDisAprove(long id, int statusId)
        {
            var content = await this.GetByIdAsync(id);
            content.StatusId = statusId;
            await this.EditAsync(content);
            return "Status updated.";
        }

        public async Task<List<ContentViewMobile>> DeshboardContent()
        {
            var res = await _dbContext.Set<ContentViewMobile>()
                 .FromSqlRaw("[dbo].[sp_DeshBoardMobile]").ToListAsync();
            return res;
        }

        public Task<List<Content>> GetByContentTypeId(long contentTypeId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContentViewMobile>> RecentlyAddedContent(int index, int size)
        {
            ContentViewMobile contentViewMobile = new ContentViewMobile();
            index = index * size;
            var Index = new SqlParameter("@Index", index);
            var Size = new SqlParameter("@Size", size);
            var StatusId = new SqlParameter("@StatusId", (int)ContentStatus.Aprove);
            List<ContentViewMobile> result = await _dbContext.Set<ContentViewMobile>()
                 .FromSqlRaw("[dbo].[sp_RecentlyAdded] {0},{1},{2}", Index, Size, StatusId).ToListAsync();
            return result;
        }

        public async Task<ContentSearchVm> SearchAdminAsync(string text, int index, int size, int contentTypeId, int catagoryId, int statusId)
        {
            ContentSearchVm contentSearchVm = new ContentSearchVm();
            index = index * size;
            if (string.IsNullOrEmpty(text))
            {
                text = "";
            }
            contentSearchVm.Total = await _dbContext.Contents.
                Where(x => x.Title.Contains(text) && x.ContentTypeId == contentTypeId
                && x.ContentType.CatagoryId ==catagoryId && x.StatusId == statusId).CountAsync();
            contentSearchVm.Contents = await _dbContext.Contents.
                Where(x => x.Title.Contains(text) && x.ContentTypeId == contentTypeId
                && x.ContentType.CatagoryId == catagoryId && x.StatusId == statusId).Skip(index).Take(size).ToListAsync();
            return contentSearchVm;
        }

        public async Task<ContentSearchVm> SearchAsync(string text, int index, int size, int contentTypeId)
        {
            ContentSearchVm contentSearchVm = new ContentSearchVm();
            List<ContentMbVm> contentMbVms = new List<ContentMbVm>();
            index = index * size;
            if (string.IsNullOrEmpty(text))
            {
                text = "";
            }
            contentSearchVm.Total = await _dbContext.Contents.Where(x => x.Title.Contains(text) && x.ContentTypeId == contentTypeId).CountAsync();
            contentSearchVm.Contents = await _dbContext.Contents.Where(x => x.Title.Contains(text) && x.ContentTypeId == contentTypeId)
                .Skip(index).Take(size).ToListAsync();

            return contentSearchVm;
        }

        public async Task<List<ContentMbVm>> SearchAsync(string text, int index, int size, int contentTypeId, int userId)
        {
            List<ContentMbVm> contentMbVms = new List<ContentMbVm>();
            index = index * size;
            if (string.IsNullOrEmpty(text))
            {
                text = "";
            }
            var contents = await _dbContext.Contents.Where(x => x.Title.Contains(text) && x.ContentTypeId == contentTypeId && x.StatusId == (int)ContentStatus.Aprove)
                             .Skip(index).Take(size).ToListAsync();

            foreach (var item in contents)
            {
                ContentMbVm contentMbVm = new ContentMbVm();
                var user = await _dbContext.Users.FindAsync(item.CreatedBy);
                item.ProviderName = user != null ? user.Name : "Admin";
                contentMbVm.content = item;
                if (userId > 0)
                {
                    var markAsRead = await markAsReadService.FirstOrDefaultAsync(x => x.Id == item.ContentTypeId && x.MarkedBy == userId);
                    var contentLike = await contentLikeService.FirstOrDefaultAsync(x => x.Id == item.ContentTypeId && x.LikedBy == userId);
                    contentMbVm.MarkStatus = markAsRead.Status;
                    contentMbVm.LikeStatus = contentLike.Status;
                }
                contentMbVms.Add(contentMbVm);
            }
            return contentMbVms;
        }
        public async Task<List<ContentViewMobile>> SearchMobileAsync(string text, int index, int size, int contentTypeId)
        {
            ContentViewMobile contentViewMobile = new ContentViewMobile();
            index = index * size;
            if (string.IsNullOrEmpty(text))
            {
                text = "";
            }
            var Index = new SqlParameter("@Index", index);
            var Size = new SqlParameter("@Size", size);
            var Text = new SqlParameter("@Text", text);
            var ContentTypeId = new SqlParameter("@ContentTypeId", contentTypeId);
            var StatusId = new SqlParameter("@StatusId", (int)ContentStatus.Aprove);
            List<ContentViewMobile> result = await _dbContext.Set<ContentViewMobile>()
                 .FromSqlRaw("sp_ContentViewMobile {0},{1},{2},{3},{4}", Text, Index, Size, ContentTypeId, StatusId).ToListAsync();
            return result;
        }
    }
}
 