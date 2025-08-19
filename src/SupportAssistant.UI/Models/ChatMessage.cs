using System;

namespace SupportAssistant.UI.Models;

public class ChatMessage
{
    public string Content { get; set; } = string.Empty;
    public bool IsFromUser { get; set; }
    public DateTime Timestamp { get; set; }
    public string FormattedTimestamp => Timestamp.ToString("HH:mm:ss");
}