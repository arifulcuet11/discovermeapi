using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Domain.VM
{
    public class ErpMenuVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get;set; }
        public string  Icon { get; set; }

        public List<Child> children { get; set; }

    }

    public class Child
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
