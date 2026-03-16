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
        private readonly ILoreRepo _repository;

        public LoreController(ILoreRepo repository)
        {
            _repository = repository;
        }

        // GET: Lore
        public IActionResult Index(string loreType, string searchString, string discoveryDate)
        {
            var allLores = _repository.GetAllLores();

            //LINQ - Get distinct god types
            var godTypes = allLores
                .Where(l => !string.IsNullOrEmpty(l.GodType))
                .Select(l => l.GodType)
                .Distinct()
                .OrderBy(g => g)
                .ToList();
            
            // Filter lores based on search criteria
            var lore = allLores
                .Where(l => string.IsNullOrEmpty(searchString) || l.Title!.Contains(searchString))
                .Where(l => string.IsNullOrEmpty(loreType) || l.GodType == loreType)
                .Where(l => string.IsNullOrEmpty(discoveryDate) || l.DiscoveryDate.Date == DateTime.Parse(discoveryDate).Date)
                .ToList();
            
            var loreTypesVM = new LoreTypeViewModel
            {
                Types = new SelectList(godTypes),
                Lores = lore
            };
        
            return View(loreTypesVM);
        }

        // GET: Lore/Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lore = _repository.GetLoreById(id.Value);
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
        public IActionResult Create([Bind("Id,Title,DiscoveryDate,GodType,Notes")] Lore lore)
        {
            if (string.IsNullOrEmpty(lore.Title))
            {
                ModelState.AddModelError(nameof(lore.Title),
                    "Please enter a title");
            }
            
            if (string.IsNullOrEmpty(lore.GodType))
            {
                ModelState.AddModelError(nameof(lore.GodType),
                    "Please select a god type");
            }

            if (ModelState.IsValid)
            {
                _repository.CreateLore(lore);
                _repository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(lore);
        }

        // GET: Lore/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lore = _repository.GetLoreById(id.Value);
            if (lore == null)
            {
                return NotFound();
            }
            return View(lore);
        }

        // POST: Lore/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,DiscoveryDate,GodType,Notes")] Lore lore)
        {
            if (id != lore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateLore(lore);
                    _repository.SaveChanges();
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lore = _repository.GetLoreById(id.Value);
            if (lore == null)
            {
                return NotFound();
            }

            return View(lore);
        }

        // POST: Lore/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var lore = _repository.GetLoreById(id);
            if (lore != null)
            {
                _repository.DeleteLore(lore);
            }

            _repository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool LoreExists(int id)
        {
            return _repository.GetLoreById(id) != null;
        }
    }
}
