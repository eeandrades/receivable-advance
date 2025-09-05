using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.SimulateReceivableAdvanceRequests;
public interface ISimulateReceivableAdvanceRequestHandler
{
    Task<Result<SimulateReceivableAdvanceRequestResult>> ExecuteAsync(SimulateReceivableAdvanceRequestCommand command);
}