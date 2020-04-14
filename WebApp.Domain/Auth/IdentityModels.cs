using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Domain.Auth
{
    public class IdentityModels
    {
        public class ApplicationUserRole : IdentityUserRole<long>
        {
        }

        public class ApplicationUserClaim : IdentityUserClaim<long>
        {
        }

        public class ApplicationUserLogin : IdentityUserLogin<long>
        {
        }

    }
}
