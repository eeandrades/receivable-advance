using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReceivableAdvance.Application.Commands.FinishReceivableAdvanceRequests;
public sealed class FinishReceivableAdvanceRequestHandler(IReceivableAdvanceRequestRepository repository) : IFinishReceivableAdvanceRequestHandler
{
    public async Task<Result<FinishReceivableAdvanceRequestResult>> ExecuteAsync(FinishReceivableAdvanceRequestCommand command)
    {

        var request = await repository.GetByIdAsync(command.RequestId);

        if (request is null)
        {
            return Result<FinishReceivableAdvanceRequestResult>.Create(new ReceivableAdvanceRequestNotFound(command.RequestId));
        }

        var finishNotification = command.Finish(request, DateTime.UtcNow);

        if (finishNotification.IsInvalid)
            return Result<FinishReceivableAdvanceRequestResult>.Create(finishNotification);

        await repository.UpdateAsync(request);

        var result = new FinishReceivableAdvanceRequestResult(request.Status, request.FinishDate!.Value);

        return Result<FinishReceivableAdvanceRequestResult>.Create(finishNotification, result);
    }
}

public sealed record FinishReceivableAdvanceRequestResult(RequestStatus NewStatus, DateTime FinishDate);
