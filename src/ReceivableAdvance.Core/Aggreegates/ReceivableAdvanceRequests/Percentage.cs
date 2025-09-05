namespace ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

public sealed record Percentage
{
    public decimal Value { get; }

    public Percentage(decimal value)
    {
        if (value < 0 || value > 1) throw new ArgumentOutOfRangeException(nameof(value));
        Value = value;
    }
    public static Percentage FromPercent(decimal percent) => new(percent / 100m);

    public static implicit operator decimal(Percentage percentage) => percentage.Value;
}
