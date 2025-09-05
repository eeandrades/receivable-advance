using System.Reflection.Metadata.Ecma335;

namespace ReceivableAdvance.Common.Notifications;

public record Notification(string Message, Level Level)
{
    public bool IsValid => Level.IsValid;
    public bool IsInvalid => !IsValid;
}