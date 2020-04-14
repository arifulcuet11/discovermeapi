using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core.Interfaces;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Context;

namespace WebApp.Core.Services.Mobile
{
    public class MobileCoverImageService : GenericRepository<MobileCoverImage>, IMobileCoverImage
    {
        private readonly ApplicationDbContext _dbContext;
        public MobileCoverImageService(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
