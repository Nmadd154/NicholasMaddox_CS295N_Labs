// Created by Nicholas Maddox
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Controllers;

public class BlogPostController : Controller
{
    private readonly ILogger<BlogPostController> _logger;

    public BlogPostController(ILogger<BlogPostController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}