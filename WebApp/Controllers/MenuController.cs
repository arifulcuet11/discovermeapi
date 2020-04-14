using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Services;
using WebApp.Infrastructure.Context;
using WebApp.Utility;

namespace WebApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IMenu menuService { get; set; }
        private readonly IDataProtector _protector;
        public MenuController(ApplicationDbContext dbContext, IDataProtectionProvider dataProtectionProvider)
        {
            menuService = new MenuService(dbContext);
            _protector = dataProtectionProvider.CreateProtector("");
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                var res =  menuService.GetManuList();
                return Ok(res);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
