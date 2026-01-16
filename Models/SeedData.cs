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
                return;   // DB has been seeded
            }
            context.Lore.AddRange(
                new Lore
                {
                    Title = "Margit, the Fell Omen",
                    ReleaseDate = DateTime.Parse("2022-2-25"),
                    Genre = "Demigod",
                    Price = 7.99M
                },
                new Lore
                {
                    Title = "Godrick the Grafted",
                    ReleaseDate = DateTime.Parse("2022-2-25"),
                    Genre = "Demigod",
                    Price = 8.99M
                },
                new Lore
                {
                    Title = "Radahn, Starscourge",
                    ReleaseDate = DateTime.Parse("2022-2-25"),
                    Genre = "Demigod",
                    Price = 9.99M
                },
                new Lore
                {
                    Title = "Malenia, Blade of Miquella",
                    ReleaseDate = DateTime.Parse("2022-2-25"),
                    Genre = "Demigod",
                    Price = 12.99M
                }
            );
            context.SaveChanges();
        }
    }
}