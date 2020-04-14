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

namespace WebApp.Core.Services
{
    public class ContentTypeService : GenericRepository<ContentType>, IContentType
    {
        private readonly ApplicationDbContext _dbContext;
        public ContentTypeService(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<ContentTypeVm> SearchAsync(string text, int index, int size, int catagoryId)
        {
            ContentTypeVm contentTypeVm = new ContentTypeVm();
            index = index * size;
            if (string.IsNullOrEmpty(text))
            {
                text = "";
            }
            contentTypeVm.Total = await _dbContext.ContentTypes.Where(x => x.Name.Contains(text) && x.CatagoryId == catagoryId).CountAsync();
            contentTypeVm.ContentTypes = await _dbContext.ContentTypes.Where(x => x.Name.Contains(text) && x.CatagoryId == catagoryId).Skip(index).Take(size).ToListAsync();

            return contentTypeVm;
        }
    }
}
