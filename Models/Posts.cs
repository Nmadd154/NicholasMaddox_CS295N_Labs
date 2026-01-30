// Created by Nicholas Maddox
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace MvcEldenRingBossLore.Models;

public class Posts
{
    [Key]
    public int PostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string LoreType { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Rating { get; set; } = 0;
}