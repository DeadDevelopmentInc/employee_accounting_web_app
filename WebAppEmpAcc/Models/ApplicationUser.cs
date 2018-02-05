using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebAppEmpAcc.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FrstName { get; set; }

        public string ScndName { get; set; }

        public int AccessLvl { get; set; }

        public string Department { get; set; }

        public string Sector { get; set; }

        public string Branch { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string Place { get; set; }

        public string Adress { get; set; }

        public string IdOfProfilePhoto { get; set; } 
    }
}
