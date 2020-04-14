using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Utility
{
    public class JwtAuthentication
    { 
        public string SecurityKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}
