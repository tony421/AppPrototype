using App.DatabaseContext.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DatabaseContext.Models.Production
{
    public class Store : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
