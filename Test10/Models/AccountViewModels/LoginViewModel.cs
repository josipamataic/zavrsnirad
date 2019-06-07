using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test10.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Unesite korisničko ime ili email.")]
        [Display(Name = "Email ili korisničko ime")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite lozinku.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Zapamti lozinku?")]
        public bool RememberMe { get; set; }
    }
}
