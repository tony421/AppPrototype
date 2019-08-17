using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace App.ViewModels
{
    public class AppErrorResult
    {
        public AppErrorResult()
        {
            this.Errors = new List<AppMessageResult>();
        }
        public AppErrorResult(string errorMessage) : this()
        {
            this.Errors.Add(new AppMessageResult(errorMessage));
        }
        public AppErrorResult(IdentityResult identityResult) : this()
        {
            this.Errors = identityResult.Errors.Select(s => new AppMessageResult(s.Code, s.Description)).ToList();
        }

        public List<AppMessageResult> Errors { get; set; }
    }
}
