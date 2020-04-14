using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Entities;

namespace WebApp.Domain.VM
{
    public class ContentMbVm
    {
        public Content content { get; set; }
        public int LikeStatus { get; set; }
        public int MarkStatus { get; set; }

    }
}
