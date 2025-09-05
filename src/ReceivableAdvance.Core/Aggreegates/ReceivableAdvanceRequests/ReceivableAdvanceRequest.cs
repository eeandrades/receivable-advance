namespace ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

public sealed class ReceivableAdvanceRequest(Guid id, Guid creatorId, Money requestValue, Money netValue, DateTime requestDate, RequestStatus status)
{
    public Guid Id { get; } = id;
    public Guid CreatorId { get; } = creatorId;
    public Money RequestValue { get; } = requestValue;
    public Money NetValue { get; } = netValue;
    public DateTime RequestDate { get; } = requestDate;
    public RequestStatus Status { get; } = status;

    public static async Task<ReceivableAdvanceRequest> CreatePending(Guid creatorId, Money requestValue, IReceivableAdvanceFeePolicy feePolicy)
    {
        var fee = await feePolicy.GetFeeFor(new ReceivableAdvanceFeePolicyArgs(creatorId, requestValue, DateTime.UtcNow));
        var netValue = requestValue * (1 - fee);

        return new ReceivableAdvanceRequest(Guid.NewGuid(), creatorId, requestValue, netValue, DateTime.UtcNow, RequestStatus.Pending);
    }
}
