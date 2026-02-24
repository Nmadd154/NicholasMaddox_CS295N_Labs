// Created by Nicholas Maddox
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcEldenRingBossLore.Models;

public class Blog
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.Now;
}
