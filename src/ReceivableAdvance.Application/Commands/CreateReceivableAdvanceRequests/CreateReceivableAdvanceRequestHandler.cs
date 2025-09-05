using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;

public sealed class CreateReceivableAdvanceRequestHandler(
    IReceivableAdvanceRequestRepository repository, IReceivableAdvanceFeePolicy feePolicy) : ICreateReceivableAdvanceRequestHandler
{
    public async Task<Result<CreateReceivableAdvanceRequestResult>> ExecuteAsync(CreateReceivableAdvanceRequestCommand command)
    {
        var pendingRequest = await repository.GetPendingByCreatorAsync(command.CreatorId);
        if (pendingRequest is not null)
        {
            return Result<CreateReceivableAdvanceRequestResult>.Create(new ReceivableAdvanceRequestPendingAlreadyExists());
        }

        var request = await ReceivableAdvanceRequest.CreatePending(
            command.CreatorId,
            command.RequestAmount,
            feePolicy);

        await repository.InsertAsync(request);

        var result = new CreateReceivableAdvanceRequestResult(
            request.Id,
            request.RequestDate,
            request.NetValue);

        return Result<CreateReceivableAdvanceRequestResult>.Create(new ReceivableAdvanceRequestCreatedSuccess(), result);
    }
}