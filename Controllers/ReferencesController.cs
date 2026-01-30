// Created by Nicholas Maddox
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Controllers;

public class ReferencesController : Controller
{
    private readonly ILogger<ReferencesController> _logger;

    public ReferencesController(ILogger<ReferencesController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}