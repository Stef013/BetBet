using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BetBet.ViewModels
{
    public class SettingsViewModel
    {
        [Display(Name = "Current Password: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string OldPassword { get; set; }

        [Display(Name = "New Password: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "New password can't be empty")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string NewPassword { get; set; }
        
        [Display(Name = "Confirm: ")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Amount: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Amount can't be empty")]
        public decimal Funds { get; set; }
    }
}