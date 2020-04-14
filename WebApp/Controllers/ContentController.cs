using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Services;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Context;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController: ControllerBase
    {
        private readonly IDataProtector _protector;
        private IContent contentService { get; set; }
        public ContentController(ApplicationDbContext dbContext, IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("");
            contentService = new ContentService(dbContext);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await contentService.GetByIdAsync(id);
                return Ok(res);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await contentService.GetsAsync();
                return Ok(res);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet("sub-catagory")]
        public async Task<IActionResult> GetAllByCatagoryId(int catagoryId)
        {
            try
            {
                var res = await contentService.GetsAsync();
                return Ok(res);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search(string text, int index, int size, int contentTypeId)
        {
            try
            {
                var res = await contentService.SearchAsync(text, index, size, contentTypeId);
                return Ok(res);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpGet("searchFromAPP")]
        public async Task<IActionResult> SearchFromApp(string text, int index, int size, int contentTypeId)
        {
            try
            {
                var res = await contentService.SearchMobileAsync(text, index, size, contentTypeId);
                return Ok(res);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpGet("deshboard/mobile")]
        public async Task<IActionResult> GetDeshBoardMobileData()
        {
            try
            {
                var res = await contentService.DeshboardContent();
                return Ok(res);
            }
            catch (Exception e) 
            {
                throw e;
            }

        }
        [HttpGet("recent/mobile")]
        public async Task<IActionResult> GetRecentAsync(int index, int size)
        {
            try
            {
                var res = await contentService.RecentlyAddedContent(index,size);
                return Ok(res);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpGet("search/admin")]
        [Authorize]
        public async Task<IActionResult> SearchAdmin(string text, int index, int size, int contentTypeId, int catagoryId, int statusId)
        {
            try
            {
                var res = await contentService.SearchAdminAsync(text, index, size, contentTypeId, catagoryId, statusId);
                return Ok(res);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpGet("aproval")]
        [Authorize]
        public async Task<IActionResult> AproveOrDisAprove(long id, int statusId)
        {
            try
            {
                var res = await contentService.AproveOrDisAprove(id, statusId) ;
                return Ok();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Content content)
        {
            try
            {
                content.CreatedDateUtc = DateTime.UtcNow;
                content.CreatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                var res = await contentService.AddAsync(content);
                return Ok(res);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Content content)
        {
            try
            {
                if (id > 0)
                {
                    content.UpdatedDateUtc = DateTime.UtcNow;
                    content.UpdatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                    var res = await contentService.EditAsync(content);
                    return Ok(res);
                }
                return BadRequest();
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok("role deleted.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
