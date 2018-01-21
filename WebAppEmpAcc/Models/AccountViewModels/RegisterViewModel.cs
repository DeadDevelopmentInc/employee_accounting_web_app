using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAppEmpAcc.Models.DepartmentModels;

namespace WebAppEmpAcc.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        private CurrentUserPhotoModel _userPhoto;

        [Required]
        [Display(Name = "First Name")]
        public string FirtsName { get; set; }

        [Required]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int Position { get; set; }

        public CurrentUserPhotoModel ProfilePhoto
        {
            get
            {
                return _userPhoto;
            }
            set
            {
                _userPhoto.Id = value.Id;
                _userPhoto.Name = value.Name;
                _userPhoto.Path = value.Path;
            }
        }
    }
}
