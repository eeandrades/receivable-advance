namespace ReceivableAdvance.Common.Notifications;

public static class Levels
{
    public readonly static Level Success  = new(nameof(Success), true);
    public readonly static Level Created =  new (nameof(Created), true);
    public readonly static Level BusinessError = new(nameof(BusinessError), false);
    public readonly static Level NotFound = new(nameof(NotFound), false);
}
