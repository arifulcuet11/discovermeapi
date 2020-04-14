using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Entities;

namespace WebApp.Domain.VM
{
    public class ContentSearchVm
    {
        public List<Content> Contents { get; set; }
        public int Total { get; set; }
    }
}
