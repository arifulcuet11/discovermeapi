using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApp.Domain.Auth;
using WebApp.Domain.Models;
using WebApp.Utility;

namespace WebApp.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private SignInManager<ApplicationUser> _signManager { get; set; }
        private readonly IDataProtector _protector;
        private readonly Appsettings _appSettings;
        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signManager,
            IOptions<Appsettings> appSettings,
              IDataProtectionProvider dataProtectionProvider)
        {
            _userManager = userManager;
            _signManager = signManager;
            _appSettings = appSettings.Value;
            _protector = dataProtectionProvider.CreateProtector("");
        }
        [HttpGet]
        [Route("token")]
        [Authorize]
        public  IActionResult TokenCheck()
        {
            return Ok();
        }
        private async Task CreateUser(UserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.Name))
                throw new Exception("Name required.");

            else if (string.IsNullOrEmpty(userInfo.Email))
                throw new Exception("Eamil address required.");

            else if (await _userManager.Users.AnyAsync(x => x.Email == userInfo.Email))
                throw new Exception($"'{userInfo.Email}' already taken.");

            var user = new ApplicationUser
            {
                Name = userInfo.Name,
                Email = userInfo.Email,
                UserName = userInfo.Email,
                PhoneNumber = userInfo.PhoneNumber,
                Address = userInfo.Address
            };

           

            IdentityResult result = await _userManager.CreateAsync(user, userInfo.Password);

            if (result.Succeeded)
            {
                ApplicationUser dbUser = await _userManager.FindByEmailAsync(user.Email);
            }
            else
            {
                throw new Exception(result.ToString());
            }
           
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signup")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserInfo userInfo)
        {
            try
            {
                await CreateUser(userInfo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLogIn userLogIn)
        {
            try
            {
                AccessToken accessToken = new AccessToken();
                var result = await _signManager.PasswordSignInAsync(userLogIn.Email, userLogIn.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(userLogIn.Email);
                 
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);
                    string protectorUserId = _protector.Protect(user.Id.ToString()) ;
                
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, protectorUserId),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, user.Name),
                              new Claim ("UserId", protectorUserId)
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    accessToken.StatusCode = 200;
                    accessToken.ExpireTime = DateTime.UtcNow.AddDays(7);
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    accessToken.Token = tokenHandler.WriteToken(token);
                    return Ok(accessToken);
                }
                else
                {
                  return BadRequest("Username or Password is Invalid");
                }
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
