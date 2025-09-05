using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

public sealed class ReceivableAdvanceRequest(Guid id, Guid creatorId, Money requestValue, Money netValue, DateTime requestDate, RequestStatus status, DateTime? finishDate)
{
    public Guid Id { get; } = id;
    public Guid CreatorId { get; init; } = creatorId;
    public Money RequestValue { get; } = requestValue;
    public Money NetValue { get; } = netValue;
    public DateTime RequestDate { get; } = requestDate;
    public RequestStatus Status { get; private set; } = status;
    public DateTime? FinishDate { get; private set; } = finishDate;

    public static async Task<ReceivableAdvanceRequest> CreatePending(Guid creatorId, Money requestValue, IReceivableAdvanceFeePolicy feePolicy)
    {
        var fee = await feePolicy.GetFeeFor(new ReceivableAdvanceFeePolicyArgs(creatorId, requestValue, DateTime.UtcNow));
        var netValue = requestValue * (1 - fee);

        return new ReceivableAdvanceRequest(Guid.NewGuid(), creatorId, requestValue, netValue, DateTime.UtcNow, RequestStatus.Pending, default);
    }

    public Notification Approve(DateTime approveDate) => Finish(RequestStatus.Approved, approveDate);
    public Notification Reject(DateTime rejectDate) => Finish(RequestStatus.Rejected, rejectDate);

    private Notification Finish(RequestStatus newStatus, DateTime finishDate)
    {
        if (Status != RequestStatus.Pending)
        {
            return new RequestAlreadyFinished(Id);
        }
        Status = newStatus;
        FinishDate = finishDate;
        return new RequestFinishedSuccess(Id, newStatus);
    }

}
