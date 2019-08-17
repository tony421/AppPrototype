using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Authorize]
    [ApiController]
    //[ValidateAntiForgeryToken]
    [Route("api/[controller]/[action]")]
    public class ControllerBase : Controller
    {
        public OkObjectResult AppOkRequest(string message)
        {
            return Ok(new AppResult(message));
        }
        public OkObjectResult AppOkRequest(IdentityResult identityResult)
        {
            return Ok(new AppResult(identityResult));
        }
        public OkObjectResult AppOkRequest(object dataObject)
        {
            return Ok(new AppResult(dataObject));
        }
        public OkObjectResult AppOkRequest(string message, object dataObject)
        {
            return Ok(new AppResult(true, message, dataObject));
        }
        public OkObjectResult AppOkRequest(bool succeed, string message, object dataObject)
        {
            return Ok(new AppResult(succeed, message, dataObject));
        }

        public BadRequestObjectResult AppBadRequest(string error)
        {
            return BadRequest(new AppErrorResult(error));
        }
        public BadRequestObjectResult AppBadRequest(Exception ex)
        {
            return BadRequest(new AppErrorResult(ex.Message));
        }
        public BadRequestObjectResult AppBadRequest(IdentityResult identityResult)
        {
            return BadRequest(new AppErrorResult(identityResult));
        }
    }
}
