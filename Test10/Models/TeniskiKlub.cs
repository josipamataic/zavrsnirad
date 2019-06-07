using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Test10.Models
{
    public partial class TeniskiKlub
    {
        public TeniskiKlub()
        {
            Oglas = new HashSet<Oglas>();
            Teren = new HashSet<Teren>();           
        }

        public int IdTeniskiKlub { get; set; }
        [Required(ErrorMessage = "Unesite naziv teniskog kluba.")]
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public int BrojTerena { get; set; }
        public string UpraviteljId { get; set; }
       
        public ApplicationUser Upravitelj { get; set; }
        public ICollection<Oglas> Oglas { get; set; }
        public ICollection<Teren> Teren { get; set; }
        
    }
}
