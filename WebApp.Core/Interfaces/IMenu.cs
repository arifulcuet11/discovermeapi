using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.VM;

namespace WebApp.Core.Interfaces
{
    public interface IMenu
    {
        List<ErpMenuVm> GetManuList();
    }
}
