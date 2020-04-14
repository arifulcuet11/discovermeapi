using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Services;
using WebApp.Core.Services.Mobile;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Context;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileCoverImageController : ControllerBase
    {

        private IMobileCoverImage mobileCoverImageService { get; set; }
        private readonly IDataProtector _protector;
        private IFileUpload fileUploadService { get; set; }
        public MobileCoverImageController(ApplicationDbContext dbContext, IDataProtectionProvider dataProtectionProvider,
            IConfiguration configuration)
        {
            mobileCoverImageService = new MobileCoverImageService(dbContext);
            _protector = dataProtectionProvider.CreateProtector("");
            fileUploadService = new FileUploadService();
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await mobileCoverImageService.GetByIdAsync(id);
                return Ok(res);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await mobileCoverImageService.GetsAsync();
                return Ok(res);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var mobileCoverImage = this.SaveFile();
                if (mobileCoverImage.Id == 0)
                {
                    // catagory.CreatedDateUtc = DateTime.UtcNow;
                    mobileCoverImage.CreatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                    await mobileCoverImageService.AddAsync(mobileCoverImage);
                }

                else
                {
                   // catagory.UpdatedDateUtc = DateTime.UtcNow;
                   // catagory.UpdatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                    await mobileCoverImageService.EditAsync(mobileCoverImage);
                }
                var files = HttpContext.Request.Form.Files.Count() > 0 ?
                          HttpContext.Request.Form.Files : null;
                if (files != null)
                {
                    var fileName = mobileCoverImage.Id.ToString();
                    var filepath = fileUploadService.UploadsingleFile(files, "Catagories", fileName);
                    mobileCoverImage.Url = filepath;
                    await mobileCoverImageService.EditAsync(mobileCoverImage);
                }


                return Ok();

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private MobileCoverImage SaveFileAzure()
        {
            string file_key = "image";
            var entity = new MobileCoverImage();
            try
            {
                var model = HttpContext.Request.Form["model"];

                entity = JsonConvert.DeserializeObject<MobileCoverImage>(model);

                var files = HttpContext.Request.Form.Files.Count() > 0 ?
                          HttpContext.Request.Form.Files : null;

                if (files != null && files[file_key] != null)
                {
                    var mimType = files[file_key].ContentType;
                    string fileName = files[file_key].FileName;
                    byte[] fileBytes = null;

                    if (files[file_key].Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files[file_key].CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                    }

                  //  azureStorageService.UploadFileAsync(fileName, fileBytes, mimType);

                }
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private MobileCoverImage SaveFile()
        {
            var entity = new MobileCoverImage();
            try
            {
                var model = HttpContext.Request.Form["model"];

                entity = JsonConvert.DeserializeObject<MobileCoverImage>(model);

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MobileCoverImage mobileCoverImage)
        {
            try
            {
                if (id > 0)
                {
                    await mobileCoverImageService.EditAsync(mobileCoverImage);
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
                var item = await mobileCoverImageService.GetByIdAsync(id);
               // item.IsActive = false;
                //item.UpdatedDateUtc = DateTime.UtcNow;
               // item.UpdatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                await mobileCoverImageService.EditAsync(item);
                return Ok();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
