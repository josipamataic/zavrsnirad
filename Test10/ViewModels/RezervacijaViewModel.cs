using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test10.Models;

namespace Test10.ViewModels
{
    public class RezervacijaViewModel
    {

        public int IdRezervacija { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public int TerenId { get; set; }
        public int TeniskiKlubId { get; set; }
        public string UpraviteljId { get; set; }
        public string IgracId { get; set; }

        public ApplicationUser Upravitelj { get; set; }
        public ApplicationUser Igrac { get; set; }
        public Teren Teren { get; set; }
        public TeniskiKlub TeniskiKlub { get; set; }
    }
}
