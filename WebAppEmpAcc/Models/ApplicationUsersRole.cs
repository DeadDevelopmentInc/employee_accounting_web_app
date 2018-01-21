using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models
{
    public class ApplicationUsersRole : IdentityRole
    {
        public ApplicationUsersRole() : base()
        {
        }

        public ApplicationUsersRole(string name) : base(name)
        {

        }
    }
}
