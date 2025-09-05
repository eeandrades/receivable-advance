namespace ReceivableAdvance.Common.Notifications;

public sealed class Result<TResult>
{
    private Result(Notification notification, TResult? value)
    {
        Notification = notification;
        Value = value;
    }

    public Notification Notification { get; }
    public TResult? Value { get; }
    public bool IsValid => Notification.Level.IsValid;

    public static Result<TResult> Create(Notification notification) => new(notification, default);
    public static Result<TResult> Create(Notification notification, TResult value) => new(notification, value);
}
