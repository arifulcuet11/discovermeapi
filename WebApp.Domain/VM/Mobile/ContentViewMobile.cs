using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Domain.VM
{
    public class ContentViewMobile
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProviderName { get; set; }
        public int TotalComment { get; set; }
        public int TotalLike { get; set; }
        public int TotalRead { get; set; }
        public long CatagoryId { get; set; }
        public string CatagoryName { get; set; }
        public string? CatagoryBanglaName { get; set; }
        public string ColorCode { get; set; }
        public int? ContentLikeStatus { get; set; }
        public int? MarkAsReadStatus { get; set; }
        public DateTime CreatedDateUtc { get; set; }

    }
}
