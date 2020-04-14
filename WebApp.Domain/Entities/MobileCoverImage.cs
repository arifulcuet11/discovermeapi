using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace WebApp.Domain.Entities
{
    public class MobileCoverImage
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public int Order { get; set; }
        public int Type { get; set; }
        public long CreatedBy {get;set;}
    }
}
