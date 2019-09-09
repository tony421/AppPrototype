using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Filters
{
    //public class ViewModelNullValidationAttribute : ActionFilterAttribute
    public class ViewModelNullValidationFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is IViewModel);
            if (param.Equals(default(KeyValuePair<string, object>)))
            {
                context.Result = new BadRequestObjectResult(new AppErrorResult("ViewModel param is null"));
                return;
            }
            else
            {
                IViewModel iVM = param.Value as IViewModel;
                if (iVM.IsNull())
                {
                    context.Result = new BadRequestObjectResult(new AppErrorResult("ViewModel param is null"));
                    return;
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            await next();

            // Can do something after the action executes like IActionFilter.OnActionExecuted()
            // E.g. var resultContext = await next();
        }

        /*
         * Synchronous IActionFilter Implementation
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is IViewModel);
            if (param.Equals(default(KeyValuePair<string, object>)))
            {
                context.Result = new BadRequestObjectResult(new AppErrorResult("ViewModel param is null"));
                return;
            }
            else
            {
                IViewModel iVM = param.Value as IViewModel;
                if (iVM.IsNull())
                {
                    context.Result = new BadRequestObjectResult(new AppErrorResult("ViewModel param is null"));
                    return;
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
        */
    }
}
