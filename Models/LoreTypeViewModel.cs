// Created by Nicholas Maddox
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcEldenRingBossLore.Models;

public class LoreTypeViewModel
{
    public List<Lore>? Lores { get; set; }
    public SelectList? Types { get; set; }
    public string? LoreType { get; set; }
    public string? SearchString { get; set; }
}