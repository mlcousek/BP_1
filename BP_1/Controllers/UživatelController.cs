using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BP_1.Data;
using BP_1.Models;
using Microsoft.AspNetCore.Authorization;

namespace BP_1.Controllers
{
    public class UživatelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UživatelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Uživatel
        public async Task<IActionResult> Index()
        {
              return _context.Uživatel != null ? 
                          View(await _context.Uživatel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Uživatel'  is null.");
        }

        // GET: Uživatel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Uživatel == null)
            {
                return NotFound();
            }

            var uživatel = await _context.Uživatel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uživatel == null)
            {
                return NotFound();
            }

            return View(uživatel);
        }

        // GET: Uživatel/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uživatel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Jméno,Přijmení,Věk,Výška,Váha,Id1")] Uživatel uživatel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uživatel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uživatel);
        }

        // GET: Uživatel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Uživatel == null)
            {
                return NotFound();
            }

            var uživatel = await _context.Uživatel.FindAsync(id);
            if (uživatel == null)
            {
                return NotFound();
            }
            return View(uživatel);
        }

        // POST: Uživatel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Jméno,Přijmení,Věk,Výška,Váha,Id1")] Uživatel uživatel)
        {
            if (id != uživatel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uživatel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UživatelExists(uživatel.Id))
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
            return View(uživatel);
        }

        // GET: Uživatel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Uživatel == null)
            {
                return NotFound();
            }

            var uživatel = await _context.Uživatel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uživatel == null)
            {
                return NotFound();
            }

            return View(uživatel);
        }

        // POST: Uživatel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Uživatel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Uživatel'  is null.");
            }
            var uživatel = await _context.Uživatel.FindAsync(id);
            if (uživatel != null)
            {
                _context.Uživatel.Remove(uživatel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UživatelExists(int id)
        {
          return (_context.Uživatel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
