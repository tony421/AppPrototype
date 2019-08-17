using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using App.DatabaseContext;
using App.ViewModels;
using App.DatabaseContext.Models.Master;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using App.API.Options;
using System.Text;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    public class AccountController : ControllerBase
    {
        private MasterDbContext _masterDbContext;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly JwtAuthenticationOptions _jwtAuthenticationOptions;
        
        public AccountController(IConfiguration config, MasterDbContext masterDbContext
            , UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , IOptions<JwtAuthenticationOptions> jwtAuthenticationOptions)
        {
            _masterDbContext = masterDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthenticationOptions = jwtAuthenticationOptions.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AccountVM vm)
        {
            var appUser = new ApplicationUser()
            {
                UserName = vm.Email,
                Email = vm.Email
            };

            try
            {
                IdentityResult result = await _userManager.CreateAsync(appUser, vm.Password);
                
                if (result.Succeeded)
                    return AppOkRequest(result);
                else
                    return AppBadRequest(result);

            }
            catch (Exception ex)
            {
                return AppBadRequest(ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountVM vm)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, vm.Password))
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthenticationOptions.SecurityKey));

                    var tokenDescriptor = new SecurityTokenDescriptor();
                    tokenDescriptor.Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserId", user.Id)
                    });
                    tokenDescriptor.Expires = DateTime.UtcNow.AddHours(_jwtAuthenticationOptions.TokenExpiration);
                    tokenDescriptor.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);

                    return AppOkRequest("", token);
                }
                else
                {
                    return AppBadRequest("Login credientials are not correct.");
                }
            }
            catch (Exception ex)
            {
                return AppBadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var user =  await _userManager.FindByIdAsync(this.GetUserId());
                //var user = await _userManager.GetUserAsync(this.User);

                if (user != null)
                {
                    return AppOkRequest(user);
                }
                else
                {
                    return AppBadRequest("User is not found.");
                }
            }
            catch (Exception ex)
            {
                return AppBadRequest(ex);
            }
        }

        public string GetUserId()
        {
            string userId = this.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            return userId;
        }
    }
}
