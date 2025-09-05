using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Queries.ListReceivableAdvanceRequests;
public interface IListReceivableAdvanceRequestHandler
{
    Task<Result<IEnumerable<ListReceivableAdvanceRequestResult>>> ExecuteAsync(ListReceivableAdvanceRequestQuery query);
}