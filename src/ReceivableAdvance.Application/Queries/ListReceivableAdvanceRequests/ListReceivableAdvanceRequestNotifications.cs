using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Queries.ListReceivableAdvanceRequests;

public record ReceivableAdvanceRequestNotFound(Guid creatorId) :
    Notification($"No advance requests found for this creator {creatorId}", Levels.NotFound);




