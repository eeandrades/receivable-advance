using ReceivableAdvance.Aggreegates.ReceivableAdvanceRequests;

namespace ReceivableAdvance.Application.Queries.ListReceivableAdvanceRequests;

public sealed record ListReceivableAdvanceRequestResult(
    Guid ReceivableAdvanceRequestId, 
    decimal RequestAmount, 
    decimal NetAmount, 
    DateTime RequestDate, 
    RequestStatus Status, 
    DateTime ? FinishDate);