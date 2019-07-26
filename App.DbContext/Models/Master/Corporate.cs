using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using App.DatabaseContext.Models.Common;

namespace App.DatabaseContext.Models.Master
{
    public class Corporate : BaseEntity
    {
        public string Name { get; set; }
        public string DatabaseName { get; set; }
        public int Counter { get; set; }
    }
}
