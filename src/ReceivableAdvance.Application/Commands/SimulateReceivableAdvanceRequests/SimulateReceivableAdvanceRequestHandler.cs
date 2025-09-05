using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.SimulateReceivableAdvanceRequests;

public sealed class SimulateReceivableAdvanceRequestHandler(
    IReceivableAdvanceFeePolicy feePolicy) : ISimulateReceivableAdvanceRequestHandler
{

    private Notification Validate(SimulateReceivableAdvanceRequestCommand command)
    {
        if (command.RequestAmount <= 0)
        {
            return new InvalidRequestAmount(command.RequestAmount);
        }
        return Notification.Success;
    }

    public async Task<Result<SimulateReceivableAdvanceRequestResult>> ExecuteAsync(SimulateReceivableAdvanceRequestCommand command)
    {
        var validation = Validate(command);
        if (validation.IsInvalid)
            return Result<SimulateReceivableAdvanceRequestResult>.Create(validation);

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