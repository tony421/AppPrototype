using App.DAL.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.DAL.Models
{
    public class Corporate : BaseEntity
    {
        public string Name { get; set; }
        public string DatabaseName { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
