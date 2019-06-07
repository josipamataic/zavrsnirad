using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Test10.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Oglas = new HashSet<Oglas>();
            PorukaIgracPosiljatelj = new HashSet<Poruka>();
            PorukaIgracPrimatelj = new HashSet<Poruka>();
            RezervacijaUpravitelj = new HashSet<Rezervacija>();
            RezervacijaIgrac = new HashSet<Rezervacija>();
            TeniskiKlub = new HashSet<TeniskiKlub>();

        }           
       
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime? DatumRodenja { get; set; }             

        public virtual ICollection<Oglas> Oglas { get; set; }
        public virtual ICollection<Poruka> PorukaIgracPosiljatelj { get; set; }
        public virtual ICollection<Poruka> PorukaIgracPrimatelj { get; set; }
        public virtual ICollection<Rezervacija> RezervacijaUpravitelj { get; set; }
        public virtual ICollection<Rezervacija> RezervacijaIgrac { get; set; }
        public virtual ICollection<TeniskiKlub> TeniskiKlub { get; set; }

    }
}
