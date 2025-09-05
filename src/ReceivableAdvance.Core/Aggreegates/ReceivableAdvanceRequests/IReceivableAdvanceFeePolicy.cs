namespace ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

public interface IReceivableAdvanceFeePolicy
{
    Task<Percentage> GetFeeFor(ReceivableAdvanceFeePolicyArgs args);
}
