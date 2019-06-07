using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test10.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Unesite email adresu.")]
        [EmailAddress(ErrorMessage ="Neispravna email adresa.")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Unesite korisničko ime.")]
        [StringLength(100, ErrorMessage = "{0} mora imati između {2} i {1} znakova.", MinimumLength = 6)]
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Unesite lozinku.")]
        [StringLength(100, ErrorMessage = "Lozinka mora imati između {2} i {1} znakova.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Lozinke moraju biti jednake!")]
        public string ConfirmPassword { get; set; }

        //dodala:
        [Required(ErrorMessage = "Unesite ime.")]        
        [Display(Name = "Ime")]
        public string Ime { get; set; }

        //dodala:
        [Required(ErrorMessage = "Unesite prezime.")]     
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        //dodala userroles
        [Required]
        [Display(Name = "Uloga")]
        public string Uloga { get; set; }


    }
}
