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
        if (string.IsNullOrEmpty(post.Title))
        {
            ModelState.AddModelError(nameof(post.Title),
                "Enter a title");
        }
        
        if (string.IsNullOrEmpty(post.Content))
        {
            ModelState.AddModelError(nameof(post.Content),
                "Enter content");
        }
        
        if (string.IsNullOrEmpty(post.Author))
        {
            ModelState.AddModelError(nameof(post.Author),
                "Enter an author name");
        }
        
        if (string.IsNullOrEmpty(post.LoreType))
        {
            ModelState.AddModelError(nameof(post.LoreType),
                "Select a lore type");
        }
        
        if (post.Rating < 1 || post.Rating > 5)
        {
            ModelState.AddModelError(nameof(post.Rating),
                "Rating must be between 1 and 5");
        }

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