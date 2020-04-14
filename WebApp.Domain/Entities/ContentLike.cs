using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApp.Domain.Entities
{
    public class ContentLike
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long LikedBy { get; set; }
        [Required]
        [ForeignKey("Content")]
        public long ContentId { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }

        public Content Content { get; set; }
    }
}
