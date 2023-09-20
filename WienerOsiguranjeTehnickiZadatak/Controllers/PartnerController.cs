using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WienerOsiguranjeTehnickiZadatak.Data;
using WienerOsiguranjeTehnickiZadatak.Models;

namespace WienerOsiguranjeTehnickiZadatak.Controllers
{
    public class PartnerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartnerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var totalCount = _context.Partner.Count();
            var partners = _context.Partner.Include(p => p.Domicilnost).Include(p => p.TipPartnera)
            .Skip((page - 1) * pageSize).Take(pageSize);

            if (partners.Any()) CheckIfItIsSpecialPartner(partners);

            var model = new PaginatedPartnersViewModel
            {
                Partners = partners.ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(model);
        }

        private void CheckIfItIsSpecialPartner(IQueryable<Partner> partners)
        {
            List<Polica> specialPartners = _context.Polica.Where(x => x.IznosPremije > 5000 && Convert.ToInt32(x.BrojPolice) > 5).ToList();

            foreach (var partner in partners)
            {
                for (int i = 0; i < specialPartners.Count(); i++)
                {
                    if (partner.Id == specialPartners[i].PartnerId) partner.SpecialPartner = true;
                }
            }
        }

        public async Task<IActionResult> IndexPartnerOIB(int page = 1, int pageSize = 10, string searchTerm = null)
        {
            if (String.IsNullOrEmpty(searchTerm)) return RedirectToAction("Index");

            var totalCount = _context.Partner.Count(); //
            var partners = _context.Partner.Where(x => x.OIB.Contains(searchTerm)).Include(p => p.Domicilnost).Include(p => p.TipPartnera)
            .Skip((page - 1) * pageSize).Take(pageSize);

            if (partners.Any()) CheckIfItIsSpecialPartner(partners);

            var model = new PaginatedPartnersViewModel
            {
                Partners = partners.ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(model);
        }

        public async Task<IActionResult> IndexPartnerIme(int page = 1, int pageSize = 10, string searchTerm = null)
        {
            if (String.IsNullOrEmpty(searchTerm)) return RedirectToAction("Index");

            var totalCount = _context.Partner.Count(); //
            var partners = _context.Partner.Where(x => x.Ime.Contains(searchTerm) || x.Prezime.Contains(searchTerm))
                            .Include(p => p.Domicilnost).Include(p => p.TipPartnera)
                            .Skip((page - 1) * pageSize).Take(pageSize);

            if (partners.Any()) CheckIfItIsSpecialPartner(partners);

            var model = new PaginatedPartnersViewModel
            {
                Partners = partners.ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(model);
        }

        // GET: Partner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Partner == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .Include(p => p.Domicilnost)
                .Include(p => p.TipPartnera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        [HttpPost]
        public JsonResult SpremiPolicu(Polica polica)
        {
            var result = new JsonResult(polica);
            _context.Add(polica);
            _context.SaveChangesAsync();
            return result;
        }

        // GET: Partner/Create
        public IActionResult Create()
        {
            ViewData["DomicilnostId"] = new SelectList(_context.Domicilnost, "Id", "Tip");
            ViewData["TipPartneraId"] = new SelectList(_context.TipPartnera, "Id", "Tip");
            return View();
        }

        // POST: Partner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipPartneraId,DomicilnostId,OIB,Ime,Prezime,Naziv,Spol,Adresa,EksterniBrojPartnera,DatumUnosa")] Partner partner)
        {
                _context.Add(partner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Partner/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Partner == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner.FindAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            ViewData["DomicilnostId"] = new SelectList(_context.Domicilnost, "Id", "Id", partner.DomicilnostId);
            ViewData["TipPartneraId"] = new SelectList(_context.TipPartnera, "Id", "Id", partner.TipPartneraId);
            return View(partner);
        }

        // POST: Partner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipPartneraId,DomicilnostId,OIB,Ime,Prezime,Naziv,Spol,Adresa,EksterniBrojPartnera,DatumUnosa")] Partner partner)
        {
            if (id != partner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerExists(partner.Id))
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
            ViewData["DomicilnostId"] = new SelectList(_context.Domicilnost, "Id", "Id", partner.DomicilnostId);
            ViewData["TipPartneraId"] = new SelectList(_context.TipPartnera, "Id", "Id", partner.TipPartneraId);
            return View(partner);
        }

        // GET: Partner/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Partner == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .Include(p => p.Domicilnost)
                .Include(p => p.TipPartnera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Partner == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Partner'  is null.");
            }
            var partner = await _context.Partner.FindAsync(id);
            if (partner != null)
            {
                _context.Partner.Remove(partner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerExists(int id)
        {
          return (_context.Partner?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
