using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core.Interfaces;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Context;

namespace WebApp.Core.Services
{
    public class WebAddressService : GenericRepository<WebAddress>, IWebAddress
    {
        private readonly ApplicationDbContext _dbContext;
        public WebAddressService(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

    }
}
