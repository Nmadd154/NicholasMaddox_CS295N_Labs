// Created by Nicholas Maddox
using System.ComponentModel.DataAnnotations;

namespace MvcEldenRingBossLore.Models;

public class UserVM
{
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public List<string> Roles { get; set; } = new List<string>();
    public List<string> AllRoles { get; set; } = new List<string>();

    /*public IEnumerable<AppUser> Users { get; set; }
    public IEnumerable<IdentityRole> Roles { get; set; }*/
}
