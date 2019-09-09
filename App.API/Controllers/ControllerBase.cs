using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.API.Filters;
using App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))] //***ServiceFilter attribute retrieves an instance of the AddHeaderResultServiceFilter filter from DI
    //[ValidateAntiForgeryToken]
    [Route("api/[controller]/[action]")]
    public class ControllerBase : Controller
    {
        public OkObjectResult AppOkRequest(IdentityResult identityResult)
        {
            return Ok(new AppResult(identityResult));
        }
        public OkObjectResult AppOkRequest(string message)
        {
            return Ok(new AppResult(true, message));
        }
        public OkObjectResult AppOkRequest(object dataObject)
        {
            return Ok(new AppResult(true, "", dataObject));
        }
        public OkObjectResult AppOkRequest(string message, object dataObject)
        {
            return Ok(new AppResult(true, message, dataObject));
        }

        public OkObjectResult AppFailedRequest(string message)
        {
            return Ok(new AppResult(false, message));
        }
        public OkObjectResult AppFailedRequest(object dataObject)
        {
            return Ok(new AppResult(false, "", dataObject));
        }
        public OkObjectResult AppFailedRequest(string message, object dataObject)
        {
            return Ok(new AppResult(false, message, dataObject));
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
