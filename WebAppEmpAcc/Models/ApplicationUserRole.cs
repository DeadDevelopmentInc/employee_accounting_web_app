using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEmpAcc.Data;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace WebAppEmpAcc.Models
{
    public class ApplicationUserRole : IdentityRole
    {
        public ApplicationUserRole(string name) : base(name)
        {

        }

        public string Description { get; set; }

        
    }
}
