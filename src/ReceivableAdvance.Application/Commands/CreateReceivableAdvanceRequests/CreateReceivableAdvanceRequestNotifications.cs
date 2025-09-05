using ReceivableAdvance.Common.Notifications;

namespace ReceivableAdvance.Application.Commands.CreateReceivableAdvanceRequests;

public record ReceivableAdvanceRequestCreatedSuccess() :
    Notification("Advance request created successfully", Levels.Created);

public record ReceivableAdvanceRequestPendingAlreadyExists() :
    Notification("There is already a pending request.", Levels.BusinessError);

public record InvalidRequestAmount(decimal amount) :
    Notification($"The requested amount must be greater than zero. {amount}", Levels.BusinessError);

