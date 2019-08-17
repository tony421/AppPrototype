using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Options
{
    public class JwtAuthenticationOptions
    {
        public string SecurityKey { get; set; }
        public string ClientUrl { get; set; }
        public int TokenExpiration { get; set; }
    }
}
