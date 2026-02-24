// Created by Nicholas Maddox
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcEldenRingBossLore.Data;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Controllers
{
    public class LoreController : Controller
    {
        private readonly MvcEldenRingBossLoreContext _context;

        public LoreController(MvcEldenRingBossLoreContext context)
        {
            _context = context;
        }

        // GET: Lore
        public async Task<IActionResult> Index()
        {
            if (_context.Lore == null)
            {
                return Problem("Entity set 'MvcEldenRingBossLoreContext.Lore'  is null.");
            }

            return View(await _context.Lore.ToListAsync());
        }

        // GET: Lore/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lore = await _context.Lore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lore == null)
            {
                return NotFound();
            }

            return View(lore);
        }

        // GET: Lore/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lore/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DiscoveryDate,GodType,Notes")] Lore lore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lore);
        }

        // GET: Lore/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lore = await _context.Lore.FindAsync(id);
            if (lore == null)
            {
                return NotFound();
            }
            return View(lore);
        }

        // POST: Lore/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DiscoveryDate,GodType,Notes")] Lore lore)
        {
            if (id != lore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoreExists(lore.Id))
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
            return View(lore);
        }

        // GET: Lore/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lore = await _context.Lore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lore == null)
            {
                return NotFound();
            }

            return View(lore);
        }

        // POST: Lore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lore = await _context.Lore.FindAsync(id);
            if (lore != null)
            {
                _context.Lore.Remove(lore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoreExists(int id)
        {
            return _context.Lore.Any(e => e.Id == id);
        }
    }
}
