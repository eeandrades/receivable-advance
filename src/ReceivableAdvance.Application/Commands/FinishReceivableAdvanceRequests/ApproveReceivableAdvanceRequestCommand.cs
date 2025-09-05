using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.FinishReceivableAdvanceRequests;

public record ApproveReceivableAdvanceRequestCommand(Guid requestId): FinishReceivableAdvanceRequestCommand(requestId)
{
    public override Notification Finish(ReceivableAdvanceRequest request, DateTime finishDate) => request.Approve(finishDate);
}
