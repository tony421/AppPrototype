using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using App.DAL.Models.Common;

namespace App.DAL.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        [ForeignKey("Corporate")]
        public int CorporateId { get; set; }
        public Corporate Corporate { get; set; }
    }
}
