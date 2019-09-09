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
using App.API.Filters;

namespace App.API.Controllers
{
    public class AccountController : ControllerBase
    {
        private MasterDbContext _masterDbContext;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly JwtAuthenticationOptions _jwtAuthenticationOptions;
        private readonly IdentityOptions _identityOptions;

        public AccountController(IConfiguration config, MasterDbContext masterDbContext
            , UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , IOptions<JwtAuthenticationOptions> jwtAuthenticationOptions
            , IOptions<IdentityOptions> identityOptions)
        {
            _masterDbContext = masterDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthenticationOptions = jwtAuthenticationOptions.Value;
            _identityOptions = identityOptions.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        [ViewModelNullValidationFilter]
        public async Task<IActionResult> Register(AccountVM vm)
        {
            var appUser = new ApplicationUser()
            {
                UserName = vm.Email,
                Email = vm.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, vm.Password);

            if (result.Succeeded)
                return AppOkRequest(result);
            else
                return AppOkRequest(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ViewModelNullValidationFilter]
        public async Task<IActionResult> Login(AccountVM vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, vm.Password))
            {
                // Getting the user role
                var role = await _userManager.GetRolesAsync(user);

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthenticationOptions.SecurityKey));

                var tokenDescriptor = new SecurityTokenDescriptor();
                tokenDescriptor.Subject = new ClaimsIdentity(new Claim[] {
                    //new Claim("UserId", user.Id),
                    new Claim(_identityOptions.ClaimsIdentity.UserIdClaimType, user.Id),
                    new Claim(_identityOptions.ClaimsIdentity.RoleClaimType, role.FirstOrDefault() ?? "User")
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
                return AppFailedRequest("Login credientials are not correct.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.FindByIdAsync(this.GetUserId());
            //var user = await _userManager.GetUserAsync(this.User);

            if (user != null)
            {
                return AppOkRequest(user);
            }
            else
            {
                return AppFailedRequest("User is not found.");
            }
        }

        public string GetUserId()
        {
            string userId = this.User.Claims.FirstOrDefault(c => c.Type == _identityOptions.ClaimsIdentity.UserIdClaimType)?.Value;
            return userId;
        }
    }
}
