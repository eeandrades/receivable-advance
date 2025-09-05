namespace ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

public sealed record Money
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Amount = decimal.Round(amount, 2, MidpointRounding.ToEven);
    }
    public static Money operator -(Money a, Money b) => new(a.Amount - b.Amount);
    public static Money operator *(Money a, decimal factor) => new(a.Amount * factor);

    public static implicit operator Money(decimal amount) => new (amount);
    public static implicit operator decimal(Money money) => money.Amount;
}
