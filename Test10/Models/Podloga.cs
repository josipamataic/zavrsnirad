using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test10.Models
{
    public partial class Podloga
    {
        public Podloga()
        {
            Teren = new HashSet<Teren>();
        }
        public int IdPodloga { get; set; }
        public string NazivPodloga { get; set; }

        public ICollection<Teren> Teren { get; set; }

    }
}
