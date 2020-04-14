using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Models;

namespace WebApp.Domain.Entities
{
    public class GenderType : Auditable
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
