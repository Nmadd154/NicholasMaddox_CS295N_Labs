// Created by Nicholas Maddox
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcEldenRingBossLore.Data;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Controllers
{
    public class BlogController : Controller
    {
        private readonly MvcEldenRingBossLoreContext _context;

        public BlogController(MvcEldenRingBossLoreContext context)
        {
            _context = context;
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogPosts.ToListAsync());
        }

        // GET: Blog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Content,Author,LoreType,Rating")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.CreatedAt = DateTime.Now;
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }
    }
}