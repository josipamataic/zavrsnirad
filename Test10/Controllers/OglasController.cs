using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test10.Data;
using Test10.Models;
using Test10.ViewModels;
using Microsoft.Extensions.Options;

namespace Test10.Controllers
{
    public class OglasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        

        public OglasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;          
        }

        // GET: Oglas
        public async Task<IActionResult> Index()
        {     
            var applicationDbContext = _context.Oglas.Include(o => o.Igrac).Include(o => o.TeniskiKlub);           
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Oglas
        public async Task<IActionResult> IgracOglasi() {

            if (User.IsInRole("Igrac"))
            {
                var appDbContext = _context.Oglas.Include(o => o.Igrac).Include(o => o.TeniskiKlub).Where(u => u.IgracId != _userManager.GetUserId(User)).OrderByDescending(o=>o.Datum);
                return View(await appDbContext.ToListAsync());
            }
            else
            {
                return View(nameof(Index));
            }
         
            
        }

        public async Task<IActionResult> SviOglasi(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var applicationContext = from t in _context.Oglas.Include(o => o.Igrac).Include(o => o.TeniskiKlub)
                                     select t;

            if (User.IsInRole("Igrac"))
            {
                var mojID = _userManager.GetUserId(User);
                if (!String.IsNullOrEmpty(searchString))
                {
                    applicationContext = applicationContext.Where(t => t.IgracId != mojID).Where(t => t.TeniskiKlub.Naziv.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(t => t.Datum);
                    return View(await applicationContext.ToListAsync());
                }
                var appDbContext = _context.Oglas.Include(o => o.Igrac).Include(o => o.TeniskiKlub).Where(u => u.IgracId != _userManager.GetUserId(User)).OrderByDescending(o => o.Datum);
                return View(await appDbContext.ToListAsync());
            }
            else
            {
                return View(nameof(Index));
            }


        }

        // GET: Oglas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas
                .Include(o => o.Igrac)
                .Include(o => o.TeniskiKlub)
                .SingleOrDefaultAsync(m => m.IdOglas == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        // GET: Oglas/Create
        public IActionResult Create()
        {
            // ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv");
            return View();
        }

        // POST: Oglas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOglas,NazivOglas,Opis,TeniskiKlubId")] Oglas oglas)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                oglas.IgracId = user.Id;
                oglas.Datum = DateTime.Now;
                _context.Add(oglas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id", oglas.IgracId);
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv", oglas.TeniskiKlubId);
            return View(oglas);
        }

        // GET: Oglas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas.SingleOrDefaultAsync(m => m.IdOglas == id);
            if (oglas == null)
            {
                return NotFound();
            }
            ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id", oglas.IgracId);
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv", oglas.TeniskiKlubId);
            return View(oglas);
        }

        // POST: Oglas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOglas,NazivOglas,Opis,TeniskiKlubId")] Oglas oglas)
        {
            if (id != oglas.IdOglas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    oglas.IgracId = user.Id;
                    oglas.Datum = DateTime.Now;
                    _context.Update(oglas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OglasExists(oglas.IdOglas))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //      var igraci = await _userManager.GetUsersInRoleAsync("Igrac");
            //   var korisnici = igraci.Select(u => new
            //   {
            //        Id = u.Id,
            //        ImePrezime = u.Ime + " " + u.Prezime
            //    }).Where(igraci.);
            ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id", oglas.IgracId);
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv", oglas.TeniskiKlubId);
            return View(oglas);
        }

        // GET: Oglas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas
                .Include(o => o.Igrac)
                .Include(o => o.TeniskiKlub)
                .SingleOrDefaultAsync(m => m.IdOglas == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        // POST: Oglas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oglas = await _context.Oglas.SingleOrDefaultAsync(m => m.IdOglas == id);
            _context.Oglas.Remove(oglas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OglasExists(int id)
        {
            return _context.Oglas.Any(e => e.IdOglas == id);
        }

        public async Task<IActionResult> MojiOglasi()
        {
            var applicationDbContext = _context.Oglas.Include(o => o.Igrac).Include(o => o.TeniskiKlub).OrderByDescending(o => o.Datum);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Oglas/Create
        public IActionResult MojCreate()
        {
            // ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv");
            return View();
           
        }

        // POST: Oglas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MojCreate([Bind("IdOglas,NazivOglas,Opis,TeniskiKlubId")] Oglas oglas)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                oglas.IgracId = user.Id;
                oglas.Datum = DateTime.Now;
                _context.Add(oglas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MojiOglasi));
            }
            // ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id", oglas.IgracId);
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv", oglas.TeniskiKlubId);
            //return View(oglas);
            return View();
        }

        // GET: Oglas/Edit/5
        public async Task<IActionResult> MojEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas.SingleOrDefaultAsync(m => m.IdOglas == id);
            if (oglas == null)
            {
                return NotFound();
            }
            ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id", oglas.IgracId);
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv", oglas.TeniskiKlubId);
            return View(oglas);
        }

        // POST: Oglas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MojEdit(int id, [Bind("IdOglas,NazivOglas,Opis,TeniskiKlubId")] Oglas oglas)
        {
            if (id != oglas.IdOglas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    oglas.IgracId = user.Id;
                    oglas.Datum = DateTime.Now;
                    _context.Update(oglas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OglasExists(oglas.IdOglas))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MojiOglasi));
            }
            //      var igraci = await _userManager.GetUsersInRoleAsync("Igrac");
            //   var korisnici = igraci.Select(u => new
            //   {
            //        Id = u.Id,
            //        ImePrezime = u.Ime + " " + u.Prezime
            //    }).Where(igraci.);
            ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id", oglas.IgracId);
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv", oglas.TeniskiKlubId);
            return View(oglas);
        }
        // GET: Oglas/Delete/5
        public async Task<IActionResult> MojDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas
                .Include(o => o.Igrac)
                .Include(o => o.TeniskiKlub)
                .SingleOrDefaultAsync(m => m.IdOglas == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
            //return RedirectToAction(nameof(MojiOglasi));
        }

        // POST: Oglas/Delete/5
        [HttpPost, ActionName("MojDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MojDeleteConfirmed(int id)
        {
            var oglas = await _context.Oglas.SingleOrDefaultAsync(m => m.IdOglas == id);
            _context.Oglas.Remove(oglas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MojiOglasi));
        }

    }
}
