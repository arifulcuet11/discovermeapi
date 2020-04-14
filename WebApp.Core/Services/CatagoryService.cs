using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core.Interfaces;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Context;

namespace WebApp.Core.Services
{
    public class CatagoryService : GenericRepository<Catagory>, ICatagory
    {
        private readonly ApplicationDbContext _dbContext;
        public CatagoryService(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
