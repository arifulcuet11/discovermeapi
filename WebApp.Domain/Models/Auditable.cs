using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Domain.Models
{
    public class Auditable
    {
        public Auditable()
        {
            CreatedDateUtc = DateTime.UtcNow;
        }

        [Required]
        public virtual long CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateUtc { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDateUtc { get; set; }
    }
}
