using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Models;

namespace WebApp.Domain.Entities
{
    public class Content : Auditable
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Description { get; set; }
        public string BanglaDescription { get; set; }
        [Required]
        [ForeignKey("ContentType")]
        public long ContentTypeId { get; set; }
        public long? ProvidedBy { get; set; }
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        [StringLength(256)]
        public string BanglaTitle { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public long StatusId { get; set; }
        public int TotalLike { get; set; }
        public int TotalComment { get; set; }
        public int TotalRead { get; set; }
        public long? AprovedOrRejectBy { get; set; }
        public ContentType ContentType { get; set; }

        [NotMapped]
        public string ProviderName { get; set; }
    }
}
