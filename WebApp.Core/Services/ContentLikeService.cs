﻿using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core.Interfaces;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Context;

namespace WebApp.Core.Services
{
    public class ContentLikeService : GenericRepository<ContentLike>, IContentLike
    {
        private readonly ApplicationDbContext _dbContext;
        public ContentLikeService(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
