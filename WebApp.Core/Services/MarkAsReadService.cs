using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core.Interfaces;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Context;

namespace WebApp.Core.Services
{
    public class MarkAsReadService: GenericRepository<MarkAsRead>, IMarkAsRead
    {
        private readonly ApplicationDbContext _dbContext;
        public MarkAsReadService(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
