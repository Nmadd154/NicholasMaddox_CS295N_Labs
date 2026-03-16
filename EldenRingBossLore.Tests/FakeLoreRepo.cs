// Created by Nicholas Maddox
using MvcEldenRingBossLore.Data;
using MvcEldenRingBossLore.Models;

namespace EldenRingBossLore.Tests;

/// <summary>
/// Fake repository for testing - uses in-memory list instead of database
/// </summary>
public class FakeLoreRepo : ILoreRepo
{
    private readonly List<Lore> _loreList;

    public FakeLoreRepo()
    {
        _loreList = new List<Lore>
        {
            new Lore
            {
                Id = 1,
                Title = "Radagon's Golden Order",
                DiscoveryDate = new DateTime(2026, 3, 1),
                GodType = "God",
                Notes = "He is Marika (Mega spoiler!)"
            },
            new Lore
            {
                Id = 2,
                Title = "Malenia's Scarlet Rot",
                DiscoveryDate = new DateTime(2026, 3, 1),
                GodType = "Outer God",
                Notes = "Blade of Miquella"
            },
            new Lore
            {
                Id = 3,
                Title = "Godrick the Grafted",
                DiscoveryDate = new DateTime(2026, 3, 1),
                GodType = "Demigod",
                Notes = "First major boss"
            }
        };
    }

    public IEnumerable<Lore> GetAllLores()
    {
        return _loreList;
    }

    public Lore GetLoreById(int id)
    {
        return _loreList.FirstOrDefault(l => l.Id == id)!;
    }

    public Lore CreateLore(Lore lore)
    {
        // Assign a new ID if not set
        if (lore.Id == 0)
        {
            lore.Id = _loreList.Any() ? _loreList.Max(l => l.Id) + 1 : 1;
        }
        _loreList.Add(lore);
        return lore;
    }

    public Lore UpdateLore(Lore lore)
    {
        var existingLore = _loreList.FirstOrDefault(l => l.Id == lore.Id);
        if (existingLore != null)
        {
            existingLore.Title = lore.Title;
            existingLore.DiscoveryDate = lore.DiscoveryDate;
            existingLore.GodType = lore.GodType;
            existingLore.Notes = lore.Notes;
        }
        return lore;
    }

    public Lore DeleteLore(Lore lore)
    {
        _loreList.Remove(lore);
        return lore;
    }

    public bool SaveChanges()
    {
        // In a fake repo, just return true
        return true;
    }
}
