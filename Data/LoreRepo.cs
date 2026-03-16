using MvcEldenRingBossLore.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace MvcEldenRingBossLore.Data
{
    public class LoreRepo : ILoreRepo
    {
        private MvcEldenRingBossLoreContext context;

        public LoreRepo(MvcEldenRingBossLoreContext appDbContext)
        {
            context = appDbContext;
        }

        public IEnumerable<Lore> GetAllLores()
        {
            return context.Lore.ToList();
        }

        public Lore GetLoreById(int id)
        {
            var lore = context.Lore
                .Include(l => l.Id)
                .Include(l => l.Title)
                .Include(l => l.DiscoveryDate)
                .Include(l => l.GodType)
                .Include(l => l.Notes)
                .Where(l => l.Id == id)
                .SingleOrDefault();
            return lore!;
        }

        public Lore CreateLore(Lore lore)
        {
            context.Lore.Add(lore);
            return lore;
        }

        public Lore UpdateLore(Lore lore)
        {
            context.Lore.Update(lore);
            return lore;
        }

        public Lore DeleteLore(Lore lore)
        {
            context.Lore.Remove(lore);
            return lore;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}