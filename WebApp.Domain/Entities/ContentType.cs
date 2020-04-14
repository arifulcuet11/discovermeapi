using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Models;

namespace WebApp.Domain.Entities
{
    public class ContentType: Auditable
    {
        [Key]
        public long id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public string BanglaName { get; set; }
        public string Description { get; set; }
        [ForeignKey("Catagory")]
        public long? CatagoryId { get; set; }
        public string ColorCode { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public string Url { get; set; }

        public Catagory Catagory { get; set; }
    }
}
