using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Domain.Auth
{
    public class AccessToken
    {
        [Required]
        public string Token { get; set; }
        public int? StatusCode { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
