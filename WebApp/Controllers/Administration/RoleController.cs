using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Domain.VM;
using WebApp.Utility;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {

        private RoleManager<IdentityRole<long>> _roleManager { get; set; }
        private readonly Appsettings _appSettings;
        private readonly IDataProtector _protector;
        public RoleController(RoleManager<IdentityRole<long>> roleManager, IOptions<Appsettings> appSettings, IDataProtectionProvider dataProtectionProvider)
        {
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
            _protector = dataProtectionProvider.CreateProtector("");
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var userId = _protector.Unprotect(this.User.Claims.First(i => i.Type == "UserId").Value); 
                var userName = User.FindFirstValue(ClaimTypes.Name);
                IdentityRole<long> identityRole = await _roleManager.FindByIdAsync(id.ToString());
                return Ok(identityRole);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                List<IdentityRole<long>> identityRoles = _roleManager.Roles.ToList();
                return Ok(identityRoles);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleVm roleVm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole<long> identityRole = new IdentityRole<long>
                {
                    Name = roleVm.Name
                };
                await _roleManager.CreateAsync(identityRole);
                return Ok("Created.");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RoleVm roleVm)
        {
            try
            {
                IdentityRole<long> identityRole = await _roleManager.FindByIdAsync(id.ToString());
                identityRole.Name = roleVm.Name;
                await _roleManager.UpdateNormalizedRoleNameAsync(identityRole);
                return Ok("role updated.");
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
                IdentityRole<long> identityRole = await _roleManager.FindByIdAsync(id.ToString());
                await _roleManager.DeleteAsync(identityRole);
                return Ok("role deleted.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
