// Created by Nicholas Maddox
using System.ComponentModel.DataAnnotations;

namespace MvcEldenRingBossLore.Models;

public class RegisterVM
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;
    
    public List<string> AllRoles { get; set; } = new List<string>();
}
