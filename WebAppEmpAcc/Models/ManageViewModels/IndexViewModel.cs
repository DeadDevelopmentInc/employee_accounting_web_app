using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Id { get; set; }
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
        [Display(Name = "Current position")]
        public string Position { get; set; }

        public string AccssLvl { get; set; }

        /// <summary>
        /// Depatment of current user
        /// </summary>
        [Display(Name = "Current department")]
        public string Department { get; set; }

        [Display(Name = "Current sector")]
        public string Sector { get; set; }

        [Display(Name = "Current branch")]
        public string Branch { get; set; }

        /// <summary>
        /// Cabinet of current user
        /// </summary>
        [Display(Name = "Current place (Cabinet)")]
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

        public string StatusMessage { get; set; }

        public string ProfilePhoto { get; set; }

        [Display(Name ="Code for move to another department")]
        public string CodeForChangePosition { get; set; }

    }
}
