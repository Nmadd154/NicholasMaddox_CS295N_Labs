using MvcEldenRingBossLore.Models;

namespace MvcEldenRingBossLore.Data
{
    public interface ILoreRepo
    {
        IEnumerable<Lore> GetAllLores();
        Lore GetLoreById(int id);
        Lore CreateLore(Lore lore);
        Lore UpdateLore(Lore lore);
        Lore DeleteLore(Lore lore);
        bool SaveChanges();
    }
}