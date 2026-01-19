// Created by Nicholas Maddox
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcEldenRingBossLore.Models;

public class Lore
{
    public int Id { get; set; }
    public string? Title { get; set; }
    [Display(Name = "Discovery Date")]
    [DataType(DataType.Date)]
    public DateTime DiscoveryDate { get; set; }
    public string? GodType { get; set; }
    public string? Notes { get; set; }
}