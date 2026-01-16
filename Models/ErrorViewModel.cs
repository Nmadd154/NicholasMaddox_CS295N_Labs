// Created by Nicholas Maddox
namespace MvcEldenRingBossLore.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
