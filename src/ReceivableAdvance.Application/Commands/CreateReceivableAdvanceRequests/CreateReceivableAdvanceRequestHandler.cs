using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;

public sealed class CreateReceivableAdvanceRequestHandler(
    IReceivableAdvanceRequestRepository repository, IReceivableAdvanceFeePolicy feePolicy) : ICreateReceivableAdvanceRequestHandler
{
    private async Task<Notification> ValidateAsync(CreateReceivableAdvanceRequestCommand command)
    {
        if (command.RequestAmount <= 0)
        {
            return new InvalidRequestAmount(command.RequestAmount);
        }

        var pendingRequest = await repository.GetPendingByCreatorAsync(command.CreatorId);
        if (pendingRequest is not null)
        {
            return new ReceivableAdvanceRequestPendingAlreadyExists();
        }

        return Notification.Success;
    }


    public async Task<Result<CreateReceivableAdvanceRequestResult>> ExecuteAsync(CreateReceivableAdvanceRequestCommand command)
    {
        var validation = await ValidateAsync(command);
        if (validation.IsInvalid)
            return Result<CreateReceivableAdvanceRequestResult>.Create(validation);

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