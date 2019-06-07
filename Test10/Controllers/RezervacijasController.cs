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
    public class RezervacijasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervacijasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezervacijas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rezervacija.Include(r => r.Igrac).Include(r => r.Teren).Include(r => r.Upravitelj);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Rezervacijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija
                .Include(r => r.Igrac)
                .Include(r => r.Teren)
                .Include(r => r.Upravitelj)
                .SingleOrDefaultAsync(m => m.IdRezervacija == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // GET: Rezervacijas/Create
        public IActionResult Create()
        {
            ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TerenId"] = new SelectList(_context.Teren, "IdTeren", "NazivTerena");
            ViewData["UpraviteljId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Rezervacijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRezervacija,DatumVrijeme,Kraj,TerenId,UpraviteljId,IgracId")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id", rezervacija.IgracId);
            ViewData["TerenId"] = new SelectList(_context.Teren, "IdTeren", "NazivTerena", rezervacija.TerenId);
            ViewData["UpraviteljId"] = new SelectList(_context.Users, "Id", "Id", rezervacija.UpraviteljId);
            return View(rezervacija);
        }

        // GET: Rezervacijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija.SingleOrDefaultAsync(m => m.IdRezervacija == id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id", rezervacija.IgracId);
            ViewData["TerenId"] = new SelectList(_context.Teren, "IdTeren", "NazivTerena", rezervacija.TerenId);
            ViewData["UpraviteljId"] = new SelectList(_context.Users, "Id", "Id", rezervacija.UpraviteljId);
            return View(rezervacija);
        }

        // POST: Rezervacijas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRezervacija,DatumVrijeme,Kraj,TerenId,UpraviteljId,IgracId")] Rezervacija rezervacija)
        {
            if (id != rezervacija.IdRezervacija)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.IdRezervacija))
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
            ViewData["IgracId"] = new SelectList(_context.Users, "Id", "Id", rezervacija.IgracId);
            ViewData["TerenId"] = new SelectList(_context.Teren, "IdTeren", "NazivTerena", rezervacija.TerenId);
            ViewData["UpraviteljId"] = new SelectList(_context.Users, "Id", "Id", rezervacija.UpraviteljId);
            return View(rezervacija);
        }

        // GET: Rezervacijas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija
                .Include(r => r.Igrac)
                .Include(r => r.Teren)
                .Include(r => r.Upravitelj)
                .SingleOrDefaultAsync(m => m.IdRezervacija == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // POST: Rezervacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.Rezervacija.SingleOrDefaultAsync(m => m.IdRezervacija == id);
            _context.Rezervacija.Remove(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacija.Any(e => e.IdRezervacija == id);
        }
    }
}
