using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApp.Domain.Models;

namespace WebApp.Domain.Entities
{
    public class WebAddress : Auditable
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
