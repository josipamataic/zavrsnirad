using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Test10.Models
{
    public partial class Poruka
    {
        public int IdPoruka { get; set; }
        [Required(ErrorMessage = "Unesite tekst poruke.")]
        [MaxLength(280, ErrorMessage ="Unesite manje od 280 znakova.")]
        public string TekstPoruke { get; set; }
        [Required(ErrorMessage = "Unesite naslov poruke.")]
        [MaxLength(60, ErrorMessage = "Unesite manje od 60 znakova.")]
        public string NaslovPoruke { get; set; }
        public DateTime Vrijeme { get; set; }
        public string IgracPosiljateljId { get; set; }
        public string IgracPrimateljId { get; set; }

        public ApplicationUser IgracPosiljatelj { get; set; }
        public ApplicationUser IgracPrimatelj { get; set; }
    }
}
