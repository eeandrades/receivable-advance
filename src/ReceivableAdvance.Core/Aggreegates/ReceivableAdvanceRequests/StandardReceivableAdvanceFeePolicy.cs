namespace ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

public record StandardReceivableAdvanceFeePolicy : IReceivableAdvanceFeePolicy
{
    private readonly static Percentage StandardFee = Percentage.FromPercent(5);

    public Task<Percentage> GetFeeFor(ReceivableAdvanceFeePolicyArgs args) => Task.FromResult(StandardFee);
}
