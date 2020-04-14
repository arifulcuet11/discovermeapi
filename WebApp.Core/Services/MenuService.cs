using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Core.Interfaces;
using WebApp.Domain.VM;
using WebApp.Infrastructure.Context;

namespace WebApp.Core.Services
{
    public class MenuService : IMenu
    {
        private readonly ApplicationDbContext _dbContext;
        public MenuService(ApplicationDbContext context) 
        {
            _dbContext = context;
        }

        public List<ErpMenuVm> GetManuList()
        {
            var erpMenuList = new List<ErpMenuVm>();
            var Children1 = new List<Child>();
            Children1.Add(new Child
            {
                Url = "/content/aprove",
                Name = "Content Status",
                Icon = "p"
            });
            /////////////////////////////
            var Children2 = new List<Child>();
            Children2.Add(new Child
            {
                Url = "/content/catagory",
                Name = "Catagory",
                Icon = "p"
            });
            Children2.Add(new Child
            {
                Url = "/content/type",
                Name = "ContentType",
                Icon = "p"
            });
            //.............................................
            erpMenuList.Add(new ErpMenuVm
            {
                Id = 0,
                Name = "Dashboard",
                Url = "",
                Icon = "dashboard",
                children = null

            });
          
            erpMenuList.Add(new ErpMenuVm
            {
                Id = 1,
                Name = "Administration",
                Url = "",
                Icon = "enhanced_encryption",
                children = Children1

            });
            erpMenuList.Add(new ErpMenuVm
            {
                Id = 2,
                Name = "Configaration",
                Url = "",
                Icon = "build",
                children = Children2

            });
            var res = _dbContext.Catagories.Where(x => x.IsActive).ToList();
            var contentChildList = new List<Child>();
            foreach (var item in res)
            {
                var child = new Child();
                child.Name = item.Name;
                child.Url ="/content/type/" + item.Id;
                child.Icon = "p";
                contentChildList.Add(child);
            }
            erpMenuList.Add(new ErpMenuVm
            {
                Id = 4,
                Name = "Contents",
                Url = "",
                Icon = "format_color_fill",
                children = contentChildList

            });

            return erpMenuList;
        }
    }
}
