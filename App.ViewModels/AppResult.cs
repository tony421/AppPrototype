using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace App.ViewModels
{
    public class AppResult
    {
        public AppResult()
        {
            this.Messages = new List<AppMessageResult>();
        }
        public AppResult(IdentityResult identityResult) : this()
        {
            this.Succeeded = identityResult.Succeeded;

            if (identityResult.Errors != null && identityResult.Errors.Count() > 0)
                this.Messages = identityResult.Errors.Select(s => new AppMessageResult(s.Code, s.Description)).ToList();
        }
        public AppResult(bool succeeded, string message, object dataObject) : this()
        {
            this.Succeeded = succeeded;
            this.Messages.Add(new AppMessageResult(message));
            this.Data = dataObject;
        }
        public AppResult(bool succeeded, string message) : this(succeeded, message, null)
        {
        }
        public AppResult(string message) : this(true, message)
        {
        }
        public AppResult(object dataObject) : this()
        {
            this.Data = dataObject;
        }


        public bool Succeeded { get; set; }
        public object Data { get; set; }
        public List<AppMessageResult> Messages { get; set; }
    }
}
