using App.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Filters
{
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// In order to use DI, the filter must be added into the service collection in Startup.cs
        /// </summary>
        public CustomExceptionFilterAttribute(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                return;
            }
            else
            {
                // Logging crucial exception messages
                context.Result = new BadRequestObjectResult(new AppErrorResult("Internal error occurred! Please contact admin."));
            }
        }
    }
}
