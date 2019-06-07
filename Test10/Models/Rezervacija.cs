using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test10.Models
{
    public partial class Rezervacija
    {
        public int IdRezervacija { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public DateTime Kraj { get; set; }       

        public int TerenId { get; set; }
        public string UpraviteljId { get; set; }
        public string IgracId { get; set; }

        public ApplicationUser Upravitelj { get; set; }
        public ApplicationUser Igrac { get; set; }
        public Teren Teren { get; set; }
    }
}
