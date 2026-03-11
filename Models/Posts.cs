// Created by Nicholas Maddox
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace MvcEldenRingBossLore.Models;

public class Posts
{
    [Key]
    public int PostId { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(30, MinimumLength = 5)]
    public string LoreType { get; set; } = string.Empty;
    
    [Required]
    [StringLength(5000)]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    [StringLength(60)]
    public string Author { get; set; } = default!;
    
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Range(0, 5)]
    public int Rating { get; set; } = 0;
}