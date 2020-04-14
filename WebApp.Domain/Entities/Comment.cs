using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApp.Domain.Entities
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [ForeignKey("Content")]
        public long ContentId { get; set; }
        public string Description { get; set; }
        [Required]
        public long CommentBy { get; set; }
        [Required]
        public DateTime CommentDateTime  { get; set; }
        public Content Content { get; set; }
    }
}
