// Created by Nicholas Maddox
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcEldenRingBossLore.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEldenRingBossLore.Models;
public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcEldenRingBossLoreContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcEldenRingBossLoreContext>>()))
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any())
            {
                var users = new[]
                {
                    new { Email = "margitfan@eldenring.com", UserName = "MargitFan", Name = "Margit Fan", Password = "Margit123!" },
                    new { Email = "godrick@eldenring.com", UserName = "GodrickDaBest", Name = "Godrick DaBest!", Password = "Godrick123!" },
                    new { Email = "festival@eldenring.com", UserName = "FestivalFan", Name = "Festival of Combat Fan", Password = "Radahn123!" }
                };

                foreach (var userData in users)
                {
                    var user = new AppUser
                    {
                        UserName = userData.UserName,
                        Email = userData.Email,
                        Name = userData.Name,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user, userData.Password);
                }
            }
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
                var users = await userManager.Users.ToListAsync();
                if (users.Count >= 3)
                {
                    context.BlogPosts.AddRange(
                        new Posts
                        {
                            Title = "Margit",
                            LoreType = "Demigod",
                            Content = "Seed data test 1.",
                            Author = users[0].Name,
                            CreatedAt = DateTime.Parse("2022-2-2"),
                            Rating = 5
                        },
                        new Posts
                        {
                            Title = "Lore of Godrick",
                            LoreType = "Demigod",
                            Content = "Seed data test 2.",
                            Author = users[1].Name,
                            CreatedAt = DateTime.Parse("2022-2-2"),
                            Rating = 4
                        },
                        new Posts
                        {
                            Title = "Radahn",
                            LoreType = "Demigod",
                            Content = "Seed data test 3.",
                            Author = users[2].Name,
                            CreatedAt = DateTime.Parse("2022-2-2"),
                            Rating = 5
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
