﻿using System;
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
    public class PolicaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PolicaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Polica
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Polica.Include(p => p.Partner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Polica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Polica == null)
            {
                return NotFound();
            }

            var polica = await _context.Polica
                .Include(p => p.Partner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (polica == null)
            {
                return NotFound();
            }

            return View(polica);
        }

        // GET: Polica/Create
        public IActionResult Create()
        {
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Id");
            return View();
        }

        // POST: Polica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrojPolice,IznosPremije,PartnerId")] Polica polica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(polica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Id", polica.PartnerId);
            return View(polica);
        }

        // GET: Polica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Polica == null)
            {
                return NotFound();
            }

            var polica = await _context.Polica.FindAsync(id);
            if (polica == null)
            {
                return NotFound();
            }
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Id", polica.PartnerId);
            return View(polica);
        }

        // POST: Polica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrojPolice,IznosPremije,PartnerId")] Polica polica)
        {
            if (id != polica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(polica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicaExists(polica.Id))
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
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Id", polica.PartnerId);
            return View(polica);
        }

        // GET: Polica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Polica == null)
            {
                return NotFound();
            }

            var polica = await _context.Polica
                .Include(p => p.Partner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (polica == null)
            {
                return NotFound();
            }

            return View(polica);
        }

        // POST: Polica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Polica == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Polica'  is null.");
            }
            var polica = await _context.Polica.FindAsync(id);
            if (polica != null)
            {
                _context.Polica.Remove(polica);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicaExists(int id)
        {
          return (_context.Polica?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
