// Created by Nicholas Maddox
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcEldenRingBossLore.Data;
using System;
using System.Linq;

namespace MvcEldenRingBossLore.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcEldenRingBossLoreContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcEldenRingBossLoreContext>>()))
        {
            // Look for any lore entries.
            if (context.Lore.Any())
            {
                return;
            }
            context.Lore.AddRange(
                new Lore
                {
                    Title = "Margit, the Fell Omen",
                    DiscoveryDate = DateTime.Parse("2022-2-25"),
                    GodType = "Demigod",
                    Notes = "A powerful Omen who guards Stormveil Castle"
                },
                new Lore
                {
                    Title = "Godrick the Grafted",
                    DiscoveryDate = DateTime.Parse("2022-2-25"),
                    GodType = "Demigod",
                    Notes = "Lord of Stormveil Castle who grafts limbs of defeated foes"
                },
                new Lore
                {
                    Title = "Radahn, Starscourge",
                    DiscoveryDate = DateTime.Parse("2022-2-25"),
                    GodType = "Demigod",
                    Notes = "Legendary warrior who conquered the stars"
                },
                new Lore
                {
                    Title = "Malenia, Blade of Miquella",
                    DiscoveryDate = DateTime.Parse("2022-2-25"),
                    GodType = "Demigod",
                    Notes = "Undefeated swordswoman cursed with Scarlet Rot"
                }
            );
            context.SaveChanges();
        }
    }
}