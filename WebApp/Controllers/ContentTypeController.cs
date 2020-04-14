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
    public class ContentTypeController: ControllerBase
    {
        private  IContentType contentTypeService { get; set; }
        private readonly IDataProtector _protector;
        public ContentTypeController( ApplicationDbContext dbContext, IDataProtectionProvider dataProtectionProvider)
        {
            contentTypeService = new ContentTypeService(dbContext);
            _protector = dataProtectionProvider.CreateProtector("");
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await contentTypeService.GetByIdAsync(id);
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
                var res = await contentTypeService.GetsAsync();
                return Ok(res);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search(string text, int index , int size, int catagoryId)
        {
            try
            {
                var res = await contentTypeService.SearchAsync(text, index, size, catagoryId);
                return Ok(res);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet("catagory")]
        public async Task<IActionResult> GetAllByCatagoryId(int catagoryId)
        {
            try
            {
                var res = await contentTypeService.GetsAsync(x=>x.IsActive == true && x.CatagoryId == catagoryId);
                return Ok(res);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] ContentType  contentType)
        {
            try
            {
                contentType.CreatedDateUtc = DateTime.UtcNow;
                contentType.CreatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                await contentTypeService.AddAsync(contentType);
                return Ok();

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContentType contentType)
        {
            try
            {
                if (id > 0)
                {
                    contentType.UpdatedDateUtc = DateTime.UtcNow;
                    contentType.UpdatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                    await contentTypeService.EditAsync(contentType);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
              
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
              var item =  await contentTypeService.GetByIdAsync(id);
                item.IsActive = false;
                item.UpdatedDateUtc = DateTime.UtcNow;
                item.UpdatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                await contentTypeService.EditAsync(item);
                return Ok();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
