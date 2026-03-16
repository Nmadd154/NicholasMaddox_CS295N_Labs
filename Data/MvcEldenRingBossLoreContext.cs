// Created by Nicholas Maddox
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Data
{
    public class MvcEldenRingBossLoreContext : IdentityDbContext<AppUser>
    {
        public MvcEldenRingBossLoreContext (DbContextOptions<MvcEldenRingBossLoreContext> options): base(options){ }

        //public DbSet<MvcEldenRingBossLore.Models.Lore> Lore { get; set; } = default!;
        //public DbSet<MvcEldenRingBossLore.Models.Posts> BlogPosts { get; set; } = default!;
        public DbSet<Posts> BlogPosts { get; set; } = default!;
        public DbSet<AppUser> AppUsers { get; set; } = default!;
        public DbSet<Lore> Lore { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().ToTable("AspNetUsers");
        }
    }
}