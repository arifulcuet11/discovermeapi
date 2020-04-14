using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Entities;

namespace WebApp.Domain.VM
{
    public class ContentTypeVm
    {
        public List<ContentType> ContentTypes { get; set; }
        public int Total { get; set; }
    }
}
