﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApp.Domain.Models;

namespace WebApp.Domain.Entities
{
    public class Status : Auditable
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int StatusType { get; set; }
    }
}
