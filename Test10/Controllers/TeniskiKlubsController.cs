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

namespace Test10.Controllers
{
    public class TeniskiKlubsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public TeniskiKlubsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TeniskiKlubs
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var applicationContext = from t in _context.TeniskiKlub.Include(t => t.Upravitelj)
                                     select t;
            if (User.IsInRole("Upravitelj"))
            {
                var mojId = _userManager.GetUserId(User);
                if (!String.IsNullOrEmpty(searchString))
                {
                    applicationContext = _context.TeniskiKlub.Include(t => t.Upravitelj).Where(u => u.UpraviteljId == mojId).Where(t => t.Naziv.ToUpper().Contains(searchString.ToUpper()));
                    return View(await applicationContext.ToListAsync());
                }
            applicationContext = _context.TeniskiKlub.Include(t => t.Upravitelj).Where(u => u.UpraviteljId == mojId);
            return View(await applicationContext.ToListAsync());
            }
            else
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    applicationContext = applicationContext.Where(t => t.Naziv.ToUpper().Contains(searchString.ToUpper()));
                    return View(await applicationContext.ToListAsync());
                }
                var applicationDbContext = _context.TeniskiKlub.Include(t => t.Upravitelj);
                return View(await applicationDbContext.ToListAsync());
            }
            
        }

        // GET: TeniskiKlubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teniskiKlub = await _context.TeniskiKlub
                .Include(t => t.Upravitelj)
                .SingleOrDefaultAsync(m => m.IdTeniskiKlub == id);
            if (teniskiKlub == null)
            {
                return NotFound();
            }

            return View(teniskiKlub);
        }

        // GET: TeniskiKlubs/Create
        public IActionResult Create()
        {
            ViewData["UpraviteljId"] = new SelectList(_context.Users, "Id", "Ime");
            return View();
        }

        // POST: TeniskiKlubs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTeniskiKlub,Naziv,Adresa,BrojTerena")] TeniskiKlub teniskiKlub)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.GetUserAsync(User);
                teniskiKlub.UpraviteljId = user.Id;
                teniskiKlub.Upravitelj = user;
                _context.Add(teniskiKlub);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           // ViewData["UpraviteljId"] = new SelectList(_context.Users, "Id", "Id", teniskiKlub.UpraviteljId);
            return View(teniskiKlub);
        }

        // GET: TeniskiKlubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teniskiKlub = await _context.TeniskiKlub.SingleOrDefaultAsync(m => m.IdTeniskiKlub == id);
            if (teniskiKlub == null)
            {
                return NotFound();
            }
            var upravitelji = await _userManager.GetUsersInRoleAsync("Upravitelj");
            var korisnici = upravitelji.Select(u => new
            {
                Id = u.Id,
                ImePrezime = u.Ime + " " + u.Prezime
            });
            ViewData["Upravitelj"] = new SelectList(korisnici, "Id", "ImePrezime", teniskiKlub.UpraviteljId);
            return View(teniskiKlub);
        }

        // POST: TeniskiKlubs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTeniskiKlub,Naziv,Adresa,BrojTerena,UpraviteljId")] TeniskiKlub teniskiKlub)
        {
            if (id != teniskiKlub.IdTeniskiKlub)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teniskiKlub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeniskiKlubExists(teniskiKlub.IdTeniskiKlub))
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
            var upravitelji = await _userManager.GetUsersInRoleAsync("Upravitelj");
            var korisnici = upravitelji.Select(u => new
            {
                Id = u.Id,
                ImePrezime = u.Ime + " " + u.Prezime
            });
            ViewData["Upravitelj"] = new SelectList(korisnici, "Id", "ImePrezime", teniskiKlub.UpraviteljId);
            return View(teniskiKlub);
        }

        // GET: TeniskiKlubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teniskiKlub = await _context.TeniskiKlub
                .Include(t => t.Upravitelj)
                .SingleOrDefaultAsync(m => m.IdTeniskiKlub == id);
            if (teniskiKlub == null)
            {
                return NotFound();
            }

            return View(teniskiKlub);
        }

        // POST: TeniskiKlubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teniskiKlub = await _context.TeniskiKlub.SingleOrDefaultAsync(m => m.IdTeniskiKlub == id);
            _context.TeniskiKlub.Remove(teniskiKlub);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeniskiKlubExists(int id)
        {
            return _context.TeniskiKlub.Any(e => e.IdTeniskiKlub == id);
        }
    }
}
