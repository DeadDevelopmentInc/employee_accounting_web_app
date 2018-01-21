using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models.ManageViewModels
{
    public class IndexViewModel
    {
        /// <summary>
        /// Username of current user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Check confirm email
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// First name of current user
        /// </summary>
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Second name of current user
        /// </summary>
        [Required]
        [Display(Name = "Second name")]
        public string SecondName { get; set; }

        /// <summary>
        /// Position in heirarghy of headers of current user
        /// </summary>
        [Required]
        [Display(Name = "Current position")]
        public string Position { get; set; }

        [Required]
        public string AccssLvl { get; set; }

        /// <summary>
        /// Depatment of current user
        /// </summary>
        [Required]
        [Display(Name = "Current department")]
        public string DepartmentRoute { get; set; }

        /// <summary>
        /// Cabinet of current user
        /// </summary>
        [Required]
        [Display(Name = "Current place")]
        public string Place { get; set; }

        /// <summary>
        /// Email of current user
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Personal phone of current user
        /// </summary>
        [Phone]
        [Display(Name = "Personal phone number")]
        public string PersonalPhoneNumber { get; set; }

        /// <summary>
        /// Work phone of current user
        /// </summary>
        [Phone]
        [Display(Name = "Work phone number")]
        public string WorkPhoneNumber { get; set; }

        /// <summary>
        /// Adress of current user
        /// </summary>
        [Display(Name = "Adress")]
        public string Adress { get; set; }

        /// <summary>
        /// Path to photo
        /// </summary>
        public string PathToPhoto { get; set; }

        public string StatusMessage { get; set; }

        public bool IsAdmin { get; set; }

    }
}
