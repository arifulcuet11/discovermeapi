using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Services;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Context;
using Xunit;

namespace WebApp.xUnitTest.WebApp.Core.Test
{
   public  class WebAddressServiceTest 
    {
        [Fact]
        public async Task Add_writes_to_database()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
          .Options;

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var service = new WebAddressService(context);
                await service.AddAsync(new WebAddress { Url = "https://example.com/cats" });
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, await context.WebAddresses.CountAsync());
                Assert.Equal("https://example.com/cats", context.WebAddresses.SingleAsync().Result.Url);
            }
        }

        [Fact]
        public async Task Find_searches_url()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "Find_searches_url")
                 .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var service = new WebAddressService(context);
                await service.AddAsync(new WebAddress { Url = "https://example.com/cats" });
                await service.AddAsync(new WebAddress { Url = "https://example.com/catfish" });
                await service.AddAsync(new WebAddress { Url = "https://example.com/dogs" });
            }

            // Use a clean instance of the context to run the test
            using (var context = new ApplicationDbContext(options))
            {
                var service = new WebAddressService(context);
                var result = await service.GetsAsync(x=>x.Url.Contains("cat"));
                Assert.Equal(2, result.Count);
            }
        }
    }
}
