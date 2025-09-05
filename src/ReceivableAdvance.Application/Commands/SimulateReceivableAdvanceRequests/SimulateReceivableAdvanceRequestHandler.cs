using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.SimulateReceivableAdvanceRequests;

public sealed class SimulateReceivableAdvanceRequestHandler(
    IReceivableAdvanceFeePolicy feePolicy) : ISimulateReceivableAdvanceRequestHandler
{
    public async Task<Result<SimulateReceivableAdvanceRequestResult>> ExecuteAsync(SimulateReceivableAdvanceRequestCommand command)
    {

        var request = await ReceivableAdvanceRequest.CreatePending(
            command.CreatorId,
            command.RequestAmount,
            feePolicy);

        var result = new SimulateReceivableAdvanceRequestResult(
            request.RequestDate,
            request.NetValue);

        return Result<SimulateReceivableAdvanceRequestResult>.Create(new ReceivableAdvanceRequestSimulatedSuccess(), result);
    }
}