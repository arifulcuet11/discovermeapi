using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
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
using WebApp.Core.Services.AzureStorage;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Context;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private ICatagory catagoryService { get; set; }
        private IAzureStorage azureStorageService { get; set; }
        private IFileUpload fileUploadService { get; set; }
        private readonly IDataProtector _protector;
        public CatagoryController(ApplicationDbContext dbContext, IDataProtectionProvider dataProtectionProvider,
            IConfiguration configuration)
        {
            catagoryService = new CatagoryService(dbContext);
            fileUploadService = new FileUploadService();
            _protector = dataProtectionProvider.CreateProtector("");
            azureStorageService = new AzureStorageService(configuration);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await catagoryService.GetByIdAsync(id);
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
                var res = await catagoryService.GetsAsync(x => x.IsActive == true);
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
                var catagory = this.SaveFile();
                if (catagory.Id == 0)
                {
                    catagory.CreatedDateUtc = DateTime.UtcNow;
                    catagory.CreatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                    await catagoryService.AddAsync(catagory);
                }

                else
                {
                    catagory.UpdatedDateUtc = DateTime.UtcNow;
                    catagory.UpdatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                    await catagoryService.EditAsync(catagory);
                }
                var files = HttpContext.Request.Form.Files.Count() > 0 ?
                          HttpContext.Request.Form.Files : null;
                if (files != null)
                {
                    var fileName = catagory.Id.ToString();
                    var filepath = fileUploadService.UploadsingleFile(files, "Catagories", fileName);
                    catagory.Url = filepath;
                    await catagoryService.EditAsync(catagory);
                }


                return Ok();

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private Catagory SaveFileAzure()
        {
            string file_key = "image";
            var entity = new Catagory();
            try
            {
                var model = HttpContext.Request.Form["model"];

                entity = JsonConvert.DeserializeObject<Catagory>(model);

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

                    azureStorageService.UploadFileAsync(fileName, fileBytes, mimType);

                }
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private Catagory SaveFile()
        {
            var entity = new Catagory();
            try
            {
                var model = HttpContext.Request.Form["model"];

                entity = JsonConvert.DeserializeObject<Catagory>(model);

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Catagory catagory)
        {
            try
            {
                if (id > 0)
                {
                    catagory.UpdatedDateUtc = DateTime.UtcNow;
                    catagory.UpdatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                    await catagoryService.EditAsync(catagory);
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
                var item = await catagoryService.GetByIdAsync(id);
                item.IsActive = false;
                item.UpdatedDateUtc = DateTime.UtcNow;
                item.UpdatedBy = Int64.Parse(_protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value));
                await catagoryService.EditAsync(item);
                return Ok();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
