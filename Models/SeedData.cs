// Created by Nicholas Maddox
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcEldenRingBossLore.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEldenRingBossLore.Models;
public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration config)
    {
        using (var context = new MvcEldenRingBossLoreContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcEldenRingBossLoreContext>>()))
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            const string ADMIN_ROLE = "Admin", MANAGER_ROLE = "Manager", GUEST_ROLE = "Guest";
            // Create roles if they don't exist
            string[] roleNames = { ADMIN_ROLE, MANAGER_ROLE, GUEST_ROLE };
            foreach (var roleName in roleNames)
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed a Admin user from user secrets
            var adminPassword = config["SeedData:AdminPassword"];
            var adminUserName = config["SeedData:AdminUserName"];
            if (string.IsNullOrEmpty(adminUserName) || string.IsNullOrEmpty(adminPassword))
                throw new InvalidOperationException("Admin credentials not configured in user secrets.");

            var existingAdmin = await userManager.FindByEmailAsync(adminUserName);
            if (existingAdmin == null)
            {
                var adminUser = new AppUser
                {
                    Name = "The Admin",
                    UserName = adminUserName,
                    Email = adminUserName,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to create user {adminUser.UserName}: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                // Assign role
                var roleResult = await userManager.AddToRoleAsync(adminUser, ADMIN_ROLE);
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to add role '{ADMIN_ROLE}' to {adminUser.UserName}: " +
                        string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }
            }

            if (!userManager.Users.Any())
            {
                // Get passwords from user secrets
                var margitPassword = config["SeedData:MargitPassword"];
                var godrickPassword = config["SeedData:GodrickPassword"];
                var radahnPassword = config["SeedData:RadahnPassword"];
                
                if (string.IsNullOrEmpty(margitPassword) || string.IsNullOrEmpty(godrickPassword) || string.IsNullOrEmpty(radahnPassword))
                    throw new InvalidOperationException("Seed user passwords not configured in user secrets.");

                var users = new[]
                {
                    new { Email = "margitfan@eldenring.com", UserName = "MargitFan", Name = "Margit Fan", Password = margitPassword, Role = ADMIN_ROLE },
                    new { Email = "godrick@eldenring.com", UserName = "GodrickDaBest", Name = "Godrick DaBest!", Password = godrickPassword, Role = MANAGER_ROLE },
                    new { Email = "festival@eldenring.com", UserName = "FestivalFan", Name = "Festival of Combat Fan", Password = radahnPassword, Role = GUEST_ROLE }
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
                    var result = await userManager.CreateAsync(user, userData.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, userData.Role);
                    }
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
