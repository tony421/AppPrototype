using System;
using System.Collections.Generic;
using System.Text;

namespace App.ViewModels
{
    public class AppMessageResult
    {
        public AppMessageResult()
        {

        }
        public AppMessageResult(string messageDescription)
        {
            this.Description = messageDescription;
        }
        public AppMessageResult(string messageCode, string messageDescription)
        {
            this.Code = messageCode;
            this.Description = messageDescription;
        }

        public string Code { get; set; }
        public string Description { get; set; }
    }
}
