// Created by Nicholas Maddox
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Data
{
    public class MvcEldenRingBossLoreContext : DbContext
    {
        public MvcEldenRingBossLoreContext (DbContextOptions<MvcEldenRingBossLoreContext> options)
            : base(options)
        {
        }

        public DbSet<MvcEldenRingBossLore.Models.Lore> Lore { get; set; } = default!;
    }
}