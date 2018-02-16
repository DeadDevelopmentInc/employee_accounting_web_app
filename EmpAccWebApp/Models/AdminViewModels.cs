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

        public class RegisterNewUserViewModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirtsName { get; set; }

            [Required]
            [Display(Name = "Second Name")]
            public string SecondName { get; set; }

            [Required]
            [Display(Name = "Surname")]
            public string Surname { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

        public class DepartmentViewModel
        {
            public string Id { get; set; }

            [Required]
            [Display(Name = "Department name")]
            public string Name { get; set; }
            [Display(Name = "Head Full name")]
            public string HeadFullName { get; set; }
        }

        public class BranchViewModel
        {
            public string Id { get; set; }

            [Required]
            [Display(Name = "Branch name")]
            public string Name { get; set; }

            [Display(Name = "Head Full name")]
            public string HeadFullName { get; set; }

            [Required]
            [Display(Name = "Department name")]
            public string DepartmentName { get; set; }
        }

        public class SectorViewModel
        {
            public string Id { get; set; }

            [Required]
            [Display(Name = "Sector name")]
            public string Name { get; set; }
            [Display(Name = "Head Full name")]
            public string HeadFullName { get; set; }
            [Required]
            [Display(Name = "Branch name")]
            public string BranchName { get; set; }
        }

    }
}