using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Domain.Entities
{
    public class MarkAsRead
    {
        public long Id { get; set; }
        public long MarkedBy { get; set; }
        public long ContentId { get; set; }
        public int Status { get; set; }
        public DateTime MarkedDate { get; set; }
    }
}
