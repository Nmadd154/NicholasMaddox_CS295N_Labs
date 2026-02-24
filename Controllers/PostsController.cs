// Created by Nicholas Maddox
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Index()
    {
        return View(await _context.BlogPosts.ToListAsync());
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