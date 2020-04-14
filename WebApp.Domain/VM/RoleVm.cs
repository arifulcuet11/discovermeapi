using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Domain.VM
{
    public class RoleVm
    {
        [Required]
        public string Name { get; set; }
    }
}
