// Created by Nicholas Maddox
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Controllers;

public class OverviewController : Controller
{
    private readonly ILogger<OverviewController> _logger;

    public OverviewController(ILogger<OverviewController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}