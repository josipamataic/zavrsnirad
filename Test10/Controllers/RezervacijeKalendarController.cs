using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test10.Data;
using Test10.Models;

namespace Test10.Controllers
{
    [Produces("application/json")]
    [Route("api/events")]
    public class RezervacijeKalendarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervacijeKalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpPost]
        public IEnumerable<Rezervacija> Get()
        {
            var applicationDbContext = _context.Rezervacija.Include(r => r.Igrac).Include(r => r.Teren).Include(r => r.Upravitelj);
            return applicationDbContext;
        }
    }
}
       
