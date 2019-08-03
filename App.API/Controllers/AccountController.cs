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
        public async Task<object> Register(AccountVM vm)
        {
            var appUser = new ApplicationUser()
            {
                UserName = vm.Email,
                Email = vm.Email
            };

            try
            {
                var result = await _userManager.CreateAsync(appUser, vm.Password);
                if (result.Succeeded)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
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
                    tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(5);
                    tokenDescriptor.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);

                    return Ok(new { token });
                }
                else
                {
                    return BadRequest(new { message = "Email or password is incorrect." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
