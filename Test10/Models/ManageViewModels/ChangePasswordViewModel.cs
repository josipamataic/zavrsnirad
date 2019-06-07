using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test10.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="Unesite staru lozinku")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage ="Unesite novu lozinku")]
        [StringLength(20, ErrorMessage = "Lozinka mora imati barem 6 znakova.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Lozinke nisu jednake.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
