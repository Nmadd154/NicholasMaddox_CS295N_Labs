// Created by Nicholas Maddox
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Controllers;

public class TPLinkController : Controller
{
    private readonly ILogger<TPLinkController> _logger;

    public TPLinkController(ILogger<TPLinkController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}