using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;
using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Queries.ListReceivableAdvanceRequests;

public sealed class ListReceivableAdvanceRequestHandler(
    IReceivableAdvanceRequestRepository repository) : IListReceivableAdvanceRequestHandler
{
    public async Task<Result<IEnumerable<ListReceivableAdvanceRequestResult>>> ExecuteAsync(ListReceivableAdvanceRequestQuery query)
    {

        var requests = await repository.ListByCreatorIdAsync(query.CreatorId);

        if (!requests.Any())
        {
            return Result<IEnumerable<ListReceivableAdvanceRequestResult>>.Create(new ReceivableAdvanceRequestNotFound(query.CreatorId));
        }

        var result = requests.Select(r => new ListReceivableAdvanceRequestResult(
            ReceivableAdvanceRequestId: r.Id,
            RequestAmount: r.RequestValue.Amount,
            NetAmount: r.NetValue.Amount,
            RequestDate: r.RequestDate,
            Status: r.Status,
            FinishDate: r.FinishDate
        ));

        return Result<IEnumerable<ListReceivableAdvanceRequestResult>>.Create(Notification.Success, result);
    }
}