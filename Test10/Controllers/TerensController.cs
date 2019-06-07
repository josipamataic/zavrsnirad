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
    public class TerensController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public TerensController(ApplicationDbContext context, UserManager<ApplicationUser>userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Terens
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var applicationContext = from t in _context.Teren.Include(t => t.Podloga).Include(t => t.TeniskiKlub)
                         select t;
           /*if (!String.IsNullOrEmpty(searchString))
            {
                applicationContext = applicationContext.Where(t => t.TeniskiKlub.Naziv.Contains(searchString));
                return View(await applicationContext.ToListAsync());//Naziv.Contains(searchString));
            }*/
            if (User.IsInRole("Upravitelj"))
            {
                var mojID = _userManager.GetUserId(User);

                if (!String.IsNullOrEmpty(searchString))
                {
                    applicationContext = applicationContext.Where(u => u.TeniskiKlub.UpraviteljId == mojID).Where(t => t.TeniskiKlub.Naziv.ToUpper().Contains(searchString.ToUpper()));
                    return View(await applicationContext.ToListAsync());//Naziv.Contains(searchString));
                }
                applicationContext = _context.Teren.Include(t => t.Podloga).Include(t => t.TeniskiKlub).Where(u => u.TeniskiKlub.UpraviteljId == mojID);
                return View(await applicationContext.ToListAsync());
            }
            else
           {
                if (!String.IsNullOrEmpty(searchString))
                {
                    applicationContext = applicationContext.Where(t => t.TeniskiKlub.Naziv.ToUpper().Contains(searchString.ToUpper()));
                    return View(await applicationContext.ToListAsync());//Naziv.Contains(searchString));
                }
                var applicationDbContext = _context.Teren.Include(t => t.Podloga).Include(t => t.TeniskiKlub);
                return View(await applicationDbContext.ToListAsync());
           }
           
        }
        public async Task<IActionResult> IndexKluba(int id)
        {
            var applicationDbContext = _context.Teren.Include(t => t.Podloga).Include(t => t.TeniskiKlub).Where(u=>u.TeniskiKlubId==id);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Terens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teren = await _context.Teren
                .Include(t => t.Podloga)
                .Include(t => t.TeniskiKlub)
                .SingleOrDefaultAsync(m => m.IdTeren == id);
            if (teren == null)
            {
                return NotFound();
            }

            return View(teren);
        }

        // GET: Terens/Create
        public IActionResult Create()
        {
            var mojId = _userManager.GetUserId(User);
            ViewData["PodlogaId"] = new SelectList(_context.Podloga, "IdPodloga", "NazivPodloga");
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub.Include(t => t.Upravitelj).Where(u => u.UpraviteljId == mojId), "IdTeniskiKlub", "Naziv");
            return View();
        }

        // POST: Terens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTeren,NazivTerena,PodlogaId,Prostor,TeniskiKlubId")] Teren teren)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teren);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var mojId = _userManager.GetUserId(User);
           // var klubovi = _context.TeniskiKlub.Include(t => t.Upravitelj).Where(u => u.UpraviteljId == mojId);
            ViewData["PodlogaId"] = new SelectList(_context.Podloga, "IdPodloga", "NazivPodloga", teren.PodlogaId);
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub.Include(t => t.Upravitelj).Where(u => u.UpraviteljId == mojId), "IdTeniskiKlub", "Naziv", teren.TeniskiKlubId);
            return View(teren);
        }

        // GET: Terens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teren = await _context.Teren.SingleOrDefaultAsync(m => m.IdTeren == id);
            if (teren == null)
            {
                return NotFound();
            }
            ViewData["PodlogaId"] = new SelectList(_context.Podloga, "IdPodloga", "NazivPodloga", teren.PodlogaId);
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv", teren.TeniskiKlubId);
            return View(teren);
        }

        // POST: Terens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTeren,PodlogaId,Prostor,TeniskiKlubId")] Teren teren)
        {
            if (id != teren.IdTeren)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teren);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerenExists(teren.IdTeren))
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
            ViewData["PodlogaId"] = new SelectList(_context.Podloga, "IdPodloga", "IdPodloga", teren.PodlogaId);
            ViewData["TeniskiKlubId"] = new SelectList(_context.TeniskiKlub, "IdTeniskiKlub", "Naziv", teren.TeniskiKlubId);
            return View(teren);
        }

        // GET: Terens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teren = await _context.Teren
                .Include(t => t.Podloga)
                .Include(t => t.TeniskiKlub)
                .SingleOrDefaultAsync(m => m.IdTeren == id);
            if (teren == null)
            {
                return NotFound();
            }

            return View(teren);
        }

        // POST: Terens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teren = await _context.Teren.SingleOrDefaultAsync(m => m.IdTeren == id);
            _context.Teren.Remove(teren);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerenExists(int id)
        {
            return _context.Teren.Any(e => e.IdTeren == id);
        }
    }
}
