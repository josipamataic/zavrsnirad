using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Test10.Models
{
    public partial class Oglas
    {

        public int IdOglas { get; set; }
        [Required(ErrorMessage = "Unesite naziv oglasa.")]
        [MaxLength(80, ErrorMessage = "Unesite manje od 80 znakova.")]
        public string NazivOglas { get; set; }
        [Required(ErrorMessage = "Unesite opis oglasa.")]
        [MaxLength(140, ErrorMessage = "Unesite manje od 140 znakova.")]
        public string Opis { get; set; }
        public DateTime Datum { get; set; }
        public string IgracId { get; set; }
        public int TeniskiKlubId { get; set; }

        public ApplicationUser Igrac { get; set; }
        public TeniskiKlub TeniskiKlub { get; set; }
    }
}
