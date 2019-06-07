using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test10.Data;
using Test10.Models;
using Microsoft.AspNetCore.Identity;

namespace Test10.Controllers
{
    public class PorukasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public PorukasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Porukas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Poruka.Include(p => p.IgracPosiljatelj).Include(p => p.IgracPrimatelj);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> PrimljenePoruke()
        {
            var user = _userManager.GetUserId(User);
            var applicationDbContext = _context.Poruka.Include(p => p.IgracPosiljatelj).Include(p => p.IgracPrimatelj).Where(p=>p.IgracPrimateljId == user).OrderByDescending(e => e.Vrijeme);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> PoslanePoruke()
        {
            var user = _userManager.GetUserId(User);
            var applicationDbContext = _context.Poruka.Include(p => p.IgracPosiljatelj).Include(p => p.IgracPrimatelj).Where(p => p.IgracPosiljateljId == user).OrderByDescending(e=>e.Vrijeme);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Porukas/Edit/5
             public IActionResult Odgovor(string IdPosiljatelj)
             {
                ViewBag.IgracPrimateljId =  IdPosiljatelj;
                ViewBag.primateljIme = _userManager.Users.Where(u => u.Id == IdPosiljatelj).FirstOrDefault().Ime;
                ViewBag.primateljPrezime = _userManager.Users.Where(u => u.Id == IdPosiljatelj).FirstOrDefault().Prezime;
            /*   if (id == null)
               {
                   return NotFound();
               }

               var poruka = await _context.Poruka.SingleOrDefaultAsync(m => m.IdPoruka == id);
               if (poruka == null)
               {
                   return NotFound();
               } */
            //  ViewData["IgracPrimateljId"] = new SelectList(_context.Users, "Id", "Id", poruka.IgracPosiljateljId);
            return View();
             }

             // POST: Porukas/Edit/5
             // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
             // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
             [HttpPost]
             [ValidateAntiForgeryToken]
             public async Task<IActionResult> Odgovor(string IdPosiljatelj, [Bind("IdPoruka,TekstPoruke,NaslovPoruke,Vrijeme")] Poruka poruka)
             {
                 if (ModelState.IsValid)
                 {
                     var user = await _userManager.GetUserAsync(User);             
                     poruka.IgracPosiljateljId = user.Id;
                     poruka.IgracPosiljatelj = user;
                     poruka.IgracPrimateljId = IdPosiljatelj;
                     poruka.Vrijeme = DateTime.Now;
                     poruka.IgracPrimatelj = _userManager.Users.Where(a => a.Id == IdPosiljatelj).FirstOrDefault();
                     _context.Add(poruka);
                     await _context.SaveChangesAsync();
                     return RedirectToAction(nameof(PoslanePoruke));
                 }

            //  ViewData["IgracPrimateljId"] = new SelectList(_context.Users, "Id", "Id", poruka.IgracPrimateljId);
                  ViewBag.IgracPrimateljId = IdPosiljatelj;
                  return View(poruka);
             }
            
        // GET: Porukas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poruka = await _context.Poruka
                .Include(p => p.IgracPosiljatelj)
                .Include(p => p.IgracPrimatelj)
                .SingleOrDefaultAsync(m => m.IdPoruka == id);
            if (poruka == null)
            {
                return NotFound();
            }

            return View(poruka);
        }

        // GET: Porukas/Create
        public async Task<IActionResult> Create()
        {
            var igraci = await _userManager.GetUsersInRoleAsync("Igrac");
            var korisnici = igraci.Select(u => new
            {
                Id = u.Id,
                ImePrezime = u.Ime + " " + u.Prezime
            });
            ViewData["IgracPrimateljId"] = new SelectList(korisnici, "Id", "ImePrezime");
            ViewData["IgracPosiljateljId"] = new SelectList(_context.Users, "Id", "Ime");
           // ViewData["IgracPrimateljId"] = new SelectList(_context.Users, "Id", "Ime");
            return View();
        }

        // POST: Porukas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPoruka,IgracPrimateljId,NaslovPoruke,TekstPoruke")] Poruka poruka)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                poruka.Vrijeme = DateTime.Now;
                poruka.IgracPosiljateljId = user.Id;
                _context.Add(poruka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var igraci = await _userManager.GetUsersInRoleAsync("Igrac");
            var korisnici = igraci.Select(u => new
            {
                Id = u.Id,
                ImePrezime = u.Ime + " " + u.Prezime
            });
            ViewData["IgracPrimateljId"] = new SelectList(korisnici, "Id", "ImePrezime", poruka.IgracPrimateljId);
            return View(poruka);
        }

        // GET: Porukas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poruka = await _context.Poruka.SingleOrDefaultAsync(m => m.IdPoruka == id);
            if (poruka == null)
            {
                return NotFound();
            }
            ViewData["IgracPosiljateljId"] = new SelectList(_context.Users, "Id", "Id", poruka.IgracPosiljateljId);
            ViewData["IgracPrimateljId"] = new SelectList(_context.Users, "Id", "Id", poruka.IgracPrimateljId);
            return View(poruka);
        }

        // POST: Porukas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPoruka,TekstPoruke,NaslovPoruke,Vrijeme,IgracPosiljateljId,IgracPrimateljId")] Poruka poruka)
        {
            if (id != poruka.IdPoruka)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poruka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PorukaExists(poruka.IdPoruka))
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
            ViewData["IgracPosiljateljId"] = new SelectList(_context.Users, "Id", "Id", poruka.IgracPosiljateljId);
            ViewData["IgracPrimateljId"] = new SelectList(_context.Users, "Id", "Id", poruka.IgracPrimateljId);
            return View(poruka);
        }

        // GET: Porukas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poruka = await _context.Poruka
                .Include(p => p.IgracPosiljatelj)
                .Include(p => p.IgracPrimatelj)
                .SingleOrDefaultAsync(m => m.IdPoruka == id);
            if (poruka == null)
            {
                return NotFound();
            }

            return View(poruka);
        }

        // POST: Porukas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poruka = await _context.Poruka.SingleOrDefaultAsync(m => m.IdPoruka == id);
            _context.Poruka.Remove(poruka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PorukaExists(int id)
        {
            return _context.Poruka.Any(e => e.IdPoruka == id);
        }
    }
}
