using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpAccWebApp.Models
{
    public class AdminViewModels
    {
        public class LoginViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class DepartmentViewModel
        {
            public string Id { get; set; }

            [Display(Name = "Department name")]
            public string Name { get; set; }
            [Display(Name = "Head Full name")]
            public string HeadFullName { get; set; }
        }

        public class BranchViewModel
        {
            public string Id { get; set; }

            [Display(Name = "Department name")]
            public string Name { get; set; }
            [Display(Name = "Head Full name")]
            public string HeadFullName { get; set; }
            [Display(Name = "Department name")]
            public string DepartmentName { get; set; }
        }

        public class SectorViewModel
        {
            public string Id { get; set; }

            [Display(Name = "Department name")]
            public string Name { get; set; }
            [Display(Name = "Head Full name")]
            public string HeadFullName { get; set; }
            [Display(Name = "Department name")]
            public string BranchName { get; set; }
        }

    }
}