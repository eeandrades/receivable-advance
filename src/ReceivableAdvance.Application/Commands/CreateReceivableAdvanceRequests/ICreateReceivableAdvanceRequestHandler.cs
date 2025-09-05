using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;
public interface ICreateReceivableAdvanceRequestHandler
{
    Task<Result<CreateReceivableAdvanceRequestResult>> ExecuteAsync(CreateReceivableAdvanceRequestCommand command);
}