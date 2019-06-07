using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Test10.Models
{
    public partial class Teren
    {
        public Teren()
        {
            Rezervacija = new HashSet<Rezervacija>();
        }

        public int IdTeren { get; set; }
        [Required(ErrorMessage = "Unesite naziv terena.")]
        public string NazivTerena { get; set; }
        public int PodlogaId { get; set; }
        public string Prostor { get; set; }
        public int TeniskiKlubId { get; set; }
       

        public TeniskiKlub TeniskiKlub { get; set; }
        public ICollection<Rezervacija> Rezervacija { get; set; }
        public Podloga Podloga { get; set; }
    }
}
