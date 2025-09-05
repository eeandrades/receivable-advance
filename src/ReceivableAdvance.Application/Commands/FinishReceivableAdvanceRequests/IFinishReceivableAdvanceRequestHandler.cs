using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.FinishReceivableAdvanceRequests;
public interface IFinishReceivableAdvanceRequestHandler
{
    Task<Result<FinishReceivableAdvanceRequestResult>> ExecuteAsync(FinishReceivableAdvanceRequestCommand command);
}