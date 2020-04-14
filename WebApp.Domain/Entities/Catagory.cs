using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApp.Domain.Models;

namespace WebApp.Domain.Entities
{
    public class Catagory : Auditable
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string BanglaName { get; set; }
        public string Url { get; set; }
        public string ColorCode { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}
