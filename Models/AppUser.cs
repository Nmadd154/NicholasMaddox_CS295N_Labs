using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcEldenRingBossLore.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        [NotMapped]
        public IList<string> RoleNames { get; set; } = new List<string>();
    }
}