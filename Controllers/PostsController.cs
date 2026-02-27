// Created by Nicholas Maddox
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcEldenRingBossLore.Data;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Controllers;

public class PostsController : Controller
{
    private readonly MvcEldenRingBossLoreContext _context;

    public PostsController(MvcEldenRingBossLoreContext context)
    {
        _context = context;
    }

    // GET: Posts
    public async Task<IActionResult> Index(string loreType, string searchString, string createdDate)
    {
        if (_context.BlogPosts == null)
        {
            return Problem("Entity set 'MvcEldenRingBossLoreContext.BlogPosts' is null.");
        }

        //LINQ 
        IQueryable<string> loreQuery = from p in _context.BlogPosts orderby p.LoreType select p.LoreType;

        var posts = _context.BlogPosts
            .Where(p => string.IsNullOrEmpty(searchString) 
                     || p.Title.Contains(searchString) 
                     || p.Content.Contains(searchString) 
                     || p.Author.Contains(searchString))
            .Where(p => string.IsNullOrEmpty(loreType) || p.LoreType == loreType)
            .Where(p => string.IsNullOrEmpty(createdDate) || p.CreatedAt.Date == DateTime.Parse(createdDate).Date)
            .ToList();

        var postsViewModel = new LoreTypeViewModel
        {
            Types = new SelectList(await loreQuery.Distinct().ToListAsync()),
            Posts = posts
        };

        return View(postsViewModel);
    }

    // GET: Posts/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Posts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PostId,Title,Content,Author,LoreType,Rating")] Posts post)
    {
        if (ModelState.IsValid)
        {
            post.CreatedAt = DateTime.Now;
            _context.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }
}