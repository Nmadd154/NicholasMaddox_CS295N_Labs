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
            if (!context.Lore.Any())
            {
                context.Lore.AddRange(
                    new Lore
                    {
                        Title = "Margit, the Fell Omen",
                        DiscoveryDate = DateTime.Parse("2022-2-22"),
                        GodType = "Demigod",
                        Notes = "A powerful Omen who guards Stormveil Castle"
                    },
                    new Lore
                    {
                        Title = "Godrick the Grafted",
                        DiscoveryDate = DateTime.Parse("2022-2-22"),
                        GodType = "Demigod",
                        Notes = "Lord of Stormveil Castle who grafts limbs of defeated foes"
                    },
                    new Lore
                    {
                        Title = "Radahn, Starscourge",
                        DiscoveryDate = DateTime.Parse("2022-2-22"),
                        GodType = "Demigod",
                        Notes = "Legendary warrior who conquered the stars"
                    },
                    new Lore
                    {
                        Title = "Malenia, Blade of Miquella",
                        DiscoveryDate = DateTime.Parse("2022-2-22"),
                        GodType = "Demigod",
                        Notes = "Undefeated swordswoman cursed with Scarlet Rot"
                    }
                );
                context.SaveChanges();
            }

            if (!context.BlogPosts.Any())
            {
                context.BlogPosts.AddRange(
                    new Posts
                    {
                        Title = "Margit",
                        LoreType = "Demigod",
                        Content = "Seed data test 1.",
                        Author = "Margit Fan",
                        CreatedAt = DateTime.Parse("2022-2-2"),
                        Rating = 5
                    },
                    new Posts
                    {
                        Title = "Lore of Godrick",
                        LoreType = "Demigod",
                        Content = "Seed data test 2.",
                        Author = "Godrick DaBest!",
                        CreatedAt = DateTime.Parse("2022-2-2"),
                        Rating = 4
                    },
                    new Posts
                    {
                        Title = "Radahn",
                        LoreType = "Demigod",
                        Content = "Seed data test 3.",
                        Author = "Festival of Combat Fan",
                        CreatedAt = DateTime.Parse("2022-2-2"),
                        Rating = 5
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
